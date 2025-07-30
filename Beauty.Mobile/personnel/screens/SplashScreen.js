import React, {useEffect, useState} from 'react';
import {
  StyleSheet,
  View,
  Text,
  Animated,
  StatusBar,
  Easing,
} from 'react-native';
// import {useNetInfo} from '@react-native-community/netinfo';

import Colors from '../constants/Colors';

import AuthService from '../services/AuthService';

const FadeInView = (props) => {
  let opacity = new Animated.Value(0);
  useEffect(() => {
    opacity.setValue(0);
    Animated.timing(opacity, {
      toValue: 1,
      duration: 900,
      easing: Easing.circle,
      useNativeDriver: true,
    }).start();
  }, [opacity]);

  return (
    <Animated.View
      style={{
        ...props.style,
        opacity: opacity,
      }}>
      {props.children}
    </Animated.View>
  );
};

const SplashScreen = ({navigation}) => {
  //TODO: check net connectivity
  const [isNetConnected, setNetConnected] = useState(true);
  //TODO: check net connectivity
  // const netInfo = useNetInfo();
  //TODO: go to last route
  // var navigation_states = useNavigationState((state) => state);

  useEffect(() => {
    //TODO: check net connectivity
    // setNetConnected(true);
    if (isNetConnected) {
      const splash_timeOut = setTimeout(() => {
        (async () => {
          if (await AuthService.IsLoggedin()) {
            navigation.navigate('Home');
          } else {
            navigation.navigate('Login');
          }
        })();
      }, 2000);
      return () => clearTimeout(splash_timeOut);
    }
  });

  return (
    <View style={styles.backrgound}>
      <StatusBar hidden animated />
      <View style={styles.body}>
        <FadeInView style={styles.animation}>
          <Text style={styles.bodyText}>بیوتیکا</Text>
        </FadeInView>
      </View>
      <View style={styles.footer}>
        <Text style={styles.footerText}>نسخه 0.1</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  backrgound: {
    backgroundColor: Colors.carolinaBlue,
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  animation: {
    backgroundColor: Colors.carolinaBlue,
  },
  body: {
    flex: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
  bodyText: {
    fontFamily: 'splash',
    color: Colors.white,
    fontSize: 44,
  },
  footer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  footerText: {
    fontFamily: 'bnazanin',
    color: Colors.white,
    fontSize: 15,
  },
});
export default SplashScreen;
