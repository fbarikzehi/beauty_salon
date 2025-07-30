import re
import pyodbc as db
import requests
from bs4 import BeautifulSoup
from persiantools.jdatetime import JalaliDate

server = 'localhost'
database = 'Beauty.Core'
conn = db.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER=' + server +
                  ';DATABASE='+database + ';integrated security=true;Trusted_Connection=yes;')
cursor = conn.cursor()


def fetch_insert_data(calendar_id, year):
  for month in range(1, 12):
        # fetch content
        url = 'https://www.time.ir/fa/event/list/0/{0}/{1}'.format(year, month)
        request = requests.get(url)
        page_content = BeautifulSoup(request.text, "html5lib")

        # parse
        for ul in page_content.select(".eventsCurrentMonthWrapper > ul.list-unstyled"):
            for li in ul.select("li"):
                event = li.text.strip()
                date_text = li.select('span:first-child')[0].find(text=True)
                event = event.replace(date_text, '').replace('\n', '').strip()
                pattern = re.compile(r'\[[^]]*\]')
                event = re.sub(pattern, '', event)

                isHoliday = False
                if li.attrs:
                    if 'eventHoliday' not in li.attrs['class']:
                        isHoliday = False
                    else:
                        isHoliday = True
                else:
                    isHoliday = False

                day = int(date_text.split(' ')[0])
                create_calendar_date(calendar_id, event,
                                     isHoliday, year, month, day)


def create_calendar(year):
    tsql = """SET NOCOUNT ON;
                            DECLARE @generated_keys TABLE(ID smallint);
                            IF EXISTS (SELECT * FROM Setting.Calendars WHERE Year = {0})
                             BEGIN
                               SELECT Id FROM Setting.Calendars WHERE Year = {0}
                             END
                            ELSE
                             BEGIN
                               INSERT INTO Setting.Calendars(CreateDateTime, CreateUser, IsDeleted, Year)
                               OUTPUT Inserted.id INTO @generated_keys(id)
                               VALUES (?,?,?,?);
                               SELECT id FROM @generated_keys
                             END """.format(year)
    with cursor.execute(tsql, '0001-01-01 00:00:00.0000000', '00000000-0000-0000-0000-000000000000', False, year):
        print('-------------------calendar successfully created %s -------------------------------' % year)
        return cursor.fetchone()[0]


def create_calendar_date(calendar_id, event, isHoliday, year, month, day):
    # insert into data source
    tsql = """  SET NOCOUNT ON;
                            DECLARE @generated_keys TABLE(ID int);
                            IF EXISTS (SELECT * FROM Setting.CalendarDates WHERE Occasion=N'{0}' and CalendarId={1})
                             BEGIN
                               SELECT Id FROM Setting.CalendarDates WHERE Occasion=N'{0}' and CalendarId={1}
                             END
                            ELSE
                             BEGIN
                               INSERT INTO Setting.CalendarDates(IsDeleted, IsHoliday, Date, Occasion, HolidayType, CalendarId)
                               OUTPUT Inserted.id INTO @generated_keys(id)
                               VALUES (?,?,?,?,?,?);
                               SELECT id FROM @generated_keys
                             END """.format(event, calendar_id)
    date = JalaliDate(year, month, day).to_gregorian()
    with cursor.execute(tsql, False, isHoliday, date, event, 2, calendar_id):
        print('rec successfully inserted %s' % cursor.fetchone()[0])


# main- call years
for year in range(1399, 1402):
    calendar_id = create_calendar(year)
    fetch_insert_data(calendar_id, year)
