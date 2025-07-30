import AsyncStorage from '@react-native-community/async-storage';
import EndPointUrls from '../constants/EndPointUrls';

const IsLoggedin = async () => {
  try {
    var auth_token = await AsyncStorage.getItem('auth_token');
    console.log('IsLoggedin auth_token: ', auth_token);
    if (auth_token == null || auth_token == undefined) {
      return false;
    } else {
      return true;
    }
  } catch (e) {
    console.error('FindRoute:', e);
    return false;
  }
};

const Login = async (data) => {
  return fetch(EndPointUrls.baseApiUrl + EndPointUrls.login, {
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
      if (data.isSuccess) {
        AsyncStorage.setItem('auth_token', data.token);
        AsyncStorage.setItem('personnel_id', data.personnelId);
        AsyncStorage.setItem('fullname', data.fullName);
      }
      return data;
    })
    .catch(function (error) {
      return {isSuccess: false, message: 'خطایی در برنامه رخ داد'};
    });
};

const ClearAuthData = async () => {
  try {
    AsyncStorage.removeItem('auth_token');
    AsyncStorage.removeItem('personnel_id');
    AsyncStorage.removeItem('fullname');

    return true;
  } catch (error) {
    return false;
  }
};

const GetPersonnelId = async () => {
  try {
    var personnel_id = await AsyncStorage.getItem('personnel_id');
    console.log('GetPersonnelId :', personnel_id);
    return personnel_id;
  } catch (error) {
    return false;
  }
};
const GetFullName = async () => {
  try {
    var fullname = await AsyncStorage.getItem('fullname');
    return fullname;
  } catch (error) {
    return false;
  }
};
const Logout = async () => {
  try {
    await AsyncStorage.removeItem('auth_token');
    await AsyncStorage.removeItem('personnel_id');
    await AsyncStorage.removeItem('fullname');

    return true;
  } catch (error) {
    return false;
  }
};

module.exports = {
  IsLoggedin: IsLoggedin,
  Login: Login,
  ClearAuthData: ClearAuthData,
  GetFullName: GetFullName,
  GetPersonnelId: GetPersonnelId,
  Logout: Logout,
};
