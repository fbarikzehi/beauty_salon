import React, {useEffect, useState} from 'react';
import {
  StyleSheet,
  View,
  Text,
  StatusBar,
  Image,
  FlatList,
  ActivityIndicator,
  TouchableOpacity,
  TouchableHighlight,
  RefreshControl,
} from 'react-native';
import Icon from 'react-native-vector-icons/Ionicons';
import moment from 'jalali-moment';
import FlashMessage, {
  showMessage,
  hideMessage,
} from 'react-native-flash-message';
import {SwipeablePanel} from 'rn-swipeable-panel';

import Colors from '../constants/Colors';
import AuthService from '../services/AuthService';
import AppointmentService from '../services/AppointmentService';
import AppointmentCard from '../components/AppointmentCardComponent';
import Calendar from '../components/CalendarComponent';

const HomeScreen = ({navigation}) => {
  const [fullName, setFullName] = useState('');
  const [isLoading, setLoading] = useState(true);
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [services, setServices] = useState([]);
  const [personnelId, setPersonnelId] = useState('');
  const [message, setMessage] = useState('');
  const [date, setِDate] = useState(new Date().toISOString());
  const [persianDate, setِPersianDate] = useState(
    moment(new Date(), 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD'),
  );

  useEffect(() => {
    // navigation.reset({
    //   index: 0,
    //   routes: [{name: 'Home'}],
    // });
    (async () => {
      let fullName = await AuthService.GetFullName();
      setFullName(fullName);

      let personnel = await AuthService.GetPersonnelId();
      setPersonnelId(personnel);

      await getAllNotDoneByDate(personnel, date);
    })();
  }, []);

  const onNewAppointmentHandler = () => {
    alert();
  };
  const onArchiveList = () => {
    alert();
  };
  const getAllNotDoneByDate = async (p, d) => {
    setLoading(true);
    let api_formatted_date = new Date(d);
    let services = await AppointmentService.GetAllNotDoneByDate(
      p,
      api_formatted_date,
    );
    setServices(services);
    setLoading(false);
  };
  const onMessageShow = (message) => {
    setMessage(message);
    showMessage({
      message: message,
      duration: 3000,
      backgroundColor: Colors.carolinaBlue,
      color: '#fff',
      onPress: () => {
        hideMessage();
      },
    });
  };
  const onRefresh = React.useCallback(async () => {
    await getAllNotDoneByDate(personnelId, date);
  }, []);
  const onReloadList = async () => {
    await getAllNotDoneByDate(personnelId, date);
  };
  const onDoneService = async (appointmentId, appointmentServiceId) => {
    await getAllNotDoneByDate(personnelId, date);
  };
  const onDateHandler = async (dir) => {
    var new_date = new Date(date);
    if (dir) new_date.setDate(new_date.getDate() + 1);
    else new_date.setDate(new_date.getDate() - 1);
    setِDate(new_date);
    setِPersianDate(
      moment(new_date, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD'),
    );
    await getAllNotDoneByDate(personnelId, new_date);
  };

  const onOpenCalendarHandler = () => {
    setIsCalendarOpen(true);
  };

  const onCloseCalendarHandler = () => {
    setIsCalendarOpen(false);
  };
  const onClosedCalendarHandler = () => {
    setIsCalendarOpen(false);
  };
  return (
    <View style={styles.container}>
      <StatusBar hidden={false} animated />
      <View style={styles.actionBar}>
        <View style={styles.userInfo}>
          <Image
            style={styles.userIcon}
            source={require('../assets/images/avatar.png')}
          />
          <Text style={styles.userInfoFullName}>{fullName}</Text>
        </View>
        <View style={styles.actions}>
          <TouchableOpacity
            style={styles.clickAction}
            onPress={onNewAppointmentHandler}>
            <Icon name="add-circle-outline" style={styles.iconAction}></Icon>
          </TouchableOpacity>
          <TouchableOpacity style={styles.clickAction} onPress={onArchiveList}>
            <Icon
              name="file-tray-full-outline"
              style={styles.iconAction}></Icon>
          </TouchableOpacity>
          <TouchableOpacity style={styles.clickAction} onPress={onReloadList}>
            <Icon name="sync-outline" style={styles.iconAction}></Icon>
          </TouchableOpacity>
        </View>
      </View>
      <View style={styles.dateIndicator}>
        <TouchableOpacity
          style={styles.actionNextButton}
          onPress={() => onDateHandler(true)}>
          <Icon name="ios-arrow-back" style={styles.caption}></Icon>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.actionDateText}
          onPress={() => onOpenCalendarHandler()}>
          <Text style={styles.dateText}>{persianDate}</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.actionNextButton}
          onPress={() => onDateHandler(false)}>
          <Icon name="ios-arrow-forward" style={styles.caption}></Icon>
        </TouchableOpacity>
      </View>
      <View style={styles.body}>
        {isLoading ? (
          <ActivityIndicator
            style={styles.loading}
            color={Colors.black}
            animating={true}
            size="large"
          />
        ) : services.length == 0 ? (
          <TouchableHighlight
            onPress={() => onReloadList()}
            style={styles.reload}
            underlayColor="transparent">
            <Text style={styles.noAppointmentText}>
              شما نوبتی برای این تاریخ ندارید
            </Text>
          </TouchableHighlight>
        ) : (
          <FlatList
            style={{zIndex: -1}}
            data={services}
            keyExtractor={({appointmentServiceId}, index) =>
              appointmentServiceId.toString()
            }
            renderItem={(item) => (
              <AppointmentCard
                service={item.item}
                messageHandler={onMessageShow}
                onDoneServiceHandler={onDoneService}
                onUpdateActivityIndicator={setLoading}
              />
            )}
            refreshControl={
              <RefreshControl refreshing={isLoading} onRefresh={onRefresh} />
            }
          />
        )}
      </View>
      <View>
        <SwipeablePanel
          fullWidth
          isActive={isCalendarOpen}
          onClose={onClosedCalendarHandler}
          onPressCloseButton={onCloseCalendarHandler}
          style={[styles.panel, styles.calendarPanel]}>
          <Calendar />
        </SwipeablePanel>
      </View>
      <FlashMessage position="top" />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  panel: {
    borderTopLeftRadius: 10,
    borderTopRightRadius: 10,
    borderBottomLeftRadius: 0,
    borderBottomRightRadius: 0,
  },
  calendarPanel: {
    height: 500,
  },
  actionBar: {
    flex: 1,
    flexDirection: 'row-reverse',
    backgroundColor: Colors.viridianGreen,
  },
  userInfo: {
    flexDirection: 'row-reverse',
    alignItems: 'center',
  },
  userInfoFullName: {
    fontFamily: 'iransans',
    fontSize: 18,
    fontWeight: 'bold',
    color: Colors.white,
    paddingRight: 5,
  },
  userIcon: {
    width: 30,
    height: 30,
    marginRight: 10,
    marginLeft: 5,
  },
  actions: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'flex-start',
    paddingTop: 10,
    paddingLeft: 30,
  },
  clickAction: {
    width: 50,
    height: 50,
  },
  iconAction: {
    fontSize: 30,
    fontWeight: 'bold',
    textAlign: 'center',
    alignItems: 'center',
    color: Colors.white,
    marginRight: 15,
  },
  dateIndicator: {
    flexDirection: 'row',
    backgroundColor: Colors.white,
    justifyContent: 'space-between',
    paddingTop: 10,
    paddingBottom: 10,
    paddingLeft: 20,
    paddingRight: 20,
  },
  actionDateText: {
    fontFamily: 'bnazanin',
  },
  dateText: {
    alignContent: 'center',
    textAlign: 'center',
    justifyContent: 'center',
    alignItems: 'center',
    paddingTop: 4,
    fontFamily: 'bnazanin',
    fontSize: 20,
  },
  actionNextButton: {
    height: 36,
    width: 36,
    borderWidth: 1,
    borderColor: 'rgba(201,201,201,1)',
    borderRadius: 4,
    borderStyle: 'solid',
    backgroundColor: 'transparent',
    justifyContent: 'center',
    alignItems: 'center',
    flexDirection: 'row',
    borderRadius: 2,
  },
  actionPrevButton: {},
  body: {
    flex: 10,
    backgroundColor: Colors.lightGray,
  },
  loading: {
    marginTop: 200,
    justifyContent: 'center',
    alignItems: 'center',
    flexDirection: 'row',
  },
  noAppointmentText: {
    fontFamily: 'bnazanin',
    fontSize: 20,
    textAlign: 'center',
    marginTop: 200,
    color: Colors.phthaloBlue,
  },
});
export default HomeScreen;
