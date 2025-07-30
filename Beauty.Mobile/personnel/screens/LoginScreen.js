import React, {useEffect, useState} from 'react';
import {
  StyleSheet,
  View,
  Text,
  Image,
  Keyboard,
  TextInput,
  TouchableOpacity,
  NativeModules,
  Platform,
} from 'react-native';
import Icon from 'react-native-vector-icons/Ionicons';

import Colors from '../constants/Colors';

import AuthService from '../services/AuthService';

const LoginScreen = ({navigation}) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [message, setMessage] = useState('');
  const [formPosition, setformPosition] = useState(0);

  useEffect(() => {
    // navigation.reset({
    //   index: 0,
    //   routes: [{name: 'Login'}],
    // });
    Keyboard.addListener('keyboardDidShow', _keyboardDidShow);
    Keyboard.addListener('keyboardDidHide', _keyboardDidHide);
    // cleanup function
    return () => {
      Keyboard.removeListener('keyboardDidShow', _keyboardDidShow);
      Keyboard.removeListener('keyboardDidHide', _keyboardDidHide);
    };
  }, []);

  const _keyboardDidShow = () => {
    setformPosition(-250);
  };

  const _keyboardDidHide = () => {
    setformPosition(0);
  };

  const onInputFoucs = () => {};

  const onUsernameChange = (text) => {
    setUsername(text);
  };
  const onPasswordChange = (text) => {
    setPassword(text);
  };

  const onLoginHandler = () => {
    //TODO: add loader
    Keyboard.dismiss();
    var deviceId = '';
    if (Platform.OS == 'android')
      deviceId = NativeModules.PlatformConstants.Serial;
    if (Platform.OS === 'ios')
      deviceId = NativeModules.PlatformConstants.Serial;
    var data = {
      username: username,
      password: password,
      deviceId: deviceId,
      deviceType: 1,
      role: 'Personnel',
    };
    (async () => {
      let response = await AuthService.Login(data);
      if (response.isSuccess) {
        navigation.navigate('Home');
      } else {
        //TODO: message alert
        alert(response.message);
      }
    })();
  };

  return (
    <View style={styles.container}>
      <View style={styles.brand}>
        <Image
          style={styles.barndImage}
          source={require('../assets/images/brand.png')}
        />
        <Text style={styles.barndText}>سالن زیبایی آیریانا</Text>
      </View>
      <View style={[styles.form, {marginTop: formPosition}]} elevation={50}>
        <View style={styles.formGroup}>
          <Text style={styles.label}>نام کاربری</Text>
          <TextInput
            style={styles.input}
            onChangeText={onUsernameChange}
            value={username}
            autoCorrect={false}
            autoCapitalize="none"
          />
        </View>
        <View style={styles.formGroup}>
          <Text style={styles.label}>رمز عبور</Text>
          <TextInput
            style={styles.input}
            onChangeText={onPasswordChange}
            value={password}
            autoCapitalize="none"
            autoCorrect={false}
            secureTextEntry={true}
            // onSubmitEditing={Keyboard.dismiss}
          />
        </View>
        <View style={styles.formGroup}>
          <TouchableOpacity style={styles.loginButton} onPress={onLoginHandler}>
            <Text style={styles.loginButtonText}>برو به حساب کاربری من</Text>
            <Icon name="log-in" style={styles.buttonIcon}></Icon>
          </TouchableOpacity>
        </View>
        <View style={styles.formGroup}>
          <TouchableOpacity style={styles.forgetTouch} onPress={onLoginHandler}>
            <Text style={styles.forgetText}>
              رمز عبور خود را فراموش کرده ام؟
            </Text>
          </TouchableOpacity>
        </View>
      </View>
      <View style={styles.bottom}>
        <Text style={styles.bottomText}>بیوتیکا نسخه0.1</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.primary,
  },
  brand: {
    flex: 1,
    alignContent: 'center',
    alignItems: 'center',
    backgroundColor: Colors.primary,
    zIndex: 999,
    marginTop: 40,
  },
  barndText: {
    fontFamily: 'iransans',
    color: Colors.white,
    fontSize: 30,
    textAlign: 'justify',
  },
  form: {
    flex: 2,
    backgroundColor: Colors.white,
    borderRadius: 10,
    marginLeft: 10,
    marginRight: 10,
    marginBottom: -100,
    paddingTop: 30,
    zIndex: 9999,
  },
  bottom: {
    flex: 1,
    flexDirection: 'column-reverse',
    alignContent: 'center',
    alignItems: 'center',
    paddingBottom: 10,
    backgroundColor: Colors.white,
    zIndex: 99,
  },
  bottomText: {
    fontFamily: 'bnazanin',
    color: Colors.primary,
    fontSize: 15,
  },
  formGroup: {
    marginRight: 35,
    marginLeft: 35,
    marginTop: 20,
  },
  label: {
    fontFamily: 'iransans',
    color: Colors.gray,
    fontSize: 15,
    paddingRight: 5,
  },
  input: {
    padding: 10,
    marginTop: 1,
    borderWidth: 0.2,
    borderRadius: 2,
    textAlign: 'right',
  },
  loginButton: {
    marginTop: 20,
    padding: 10,
    backgroundColor: Colors.carolinaBlue,
    borderColor: 'transparent',
    alignContent: 'center',
    borderRadius: 3,
  },
  loginButtonText: {
    textAlign: 'center',
    fontFamily: 'iransans',
    fontSize: 15,
    color: Colors.white,
  },
  buttonIcon: {
    fontSize: 25,
    textAlign: 'center',
    alignItems: 'center',
    marginTop: -25,
    marginRight: -180,
    color: Colors.white,
  },
  forgetTouch: {
    marginTop: 20,
    padding: 10,
    alignContent: 'center',
  },
  forgetText: {
    textAlign: 'center',
    fontFamily: 'iransans',
    fontSize: 15,
    color: Colors.gray,
  },
});
export default LoginScreen;
