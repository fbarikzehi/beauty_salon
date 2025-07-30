import EndPointUrls from '../constants/EndPointUrls';

//GetAllNotDoneByDate
const GetAllNotDoneByDate = async (personnelId, date) => {
  return fetch(
    EndPointUrls.baseApiUrl +
      EndPointUrls.appointmentGetAllNotDoneByDate +
      'personnelId=' +
      personnelId +
      '&&date=' +
      date.toISOString().split('T')[0],
  )
    .then((response) => {
      // console.log('personnelId:', personnelId);
      // console.log('response.status:', response.status);
      // console.log('date:', date);
      if (response.status == 200) {
        return response.json();
      } else {
        return false;
      }
    })
    .then((json) => {
      // console.log(json.data);
      if (json.data != undefined) {
        return json.data;
      } else {
        return [];
      }
    })
    .catch((error) => console.error(error));
};

//Done
const Done = async (appointmentId, appointmentServiceId) => {
  var data = {
    appointmentId: appointmentId,
    appointmentServiceId: appointmentServiceId,
  };
  // console.log(data);
  return fetch(EndPointUrls.baseApiUrl + EndPointUrls.appointmentDoneService, {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })
    .then(function (response) {
      return response.json();
    })
    .then(function (data) {
      return data;
    })
    .catch(function (error) {
      console.log(
        'There has been a problem with your fetch operation: ' + error.message,
      );
      throw error;
    });
};

//exports
module.exports = {
  GetAllNotDoneByDate: GetAllNotDoneByDate,
  Done: Done,
};
