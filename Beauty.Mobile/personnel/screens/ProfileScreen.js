import React from 'react';
import {StyleSheet, View, Text, Image} from 'react-native';
import Colors from '../constants/Colors';
import Icon from 'react-native-vector-icons/Ionicons';

import AuthService from '../services/AuthService';

const ProfileScreen = ({navigation}) => {
  const onLogoutHandler = () => {
    (async () => {
      await AuthService.Logout();
      navigation.navigate('Login');
    })();
  };
  return (
    <View style={styles.container}>
      <View style={styles.top}>
        <Image
          style={styles.userIcon}
          source={require('../assets/images/avatar.png')}
        />
        <Text style={styles.nameText}>نازنین پهلوان پور</Text>
        <Text style={styles.codeText}>5456456</Text>
      </View>
      <View style={styles.stats}>
        <View style={styles.statsItem}>
          <Text style={styles.statsItemTextTop}>90</Text>
          <Text style={styles.statsItemTextBottom}>امتیاز شما</Text>
        </View>
        <View style={styles.statsItem}>
          <Text style={styles.statsItemTextTop}>11,250,000</Text>
          <Text style={styles.statsItemTextBottom}>میزان درآمد</Text>
        </View>
        <View style={styles.statsItem}>
          <Text style={styles.statsItemTextTop}>100</Text>
          <Text style={styles.statsItemTextBottom}> ساعات کاری</Text>
        </View>
      </View>
      <View style={styles.bottom}>
        <View style={styles.navItem}></View>
        <View style={styles.navItem}></View>
        <View style={styles.navItem}></View>
        <View style={styles.navItem}></View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  top: {
    flexDirection: 'row-reverse',
    backgroundColor: Colors.viridianGreen,
    height: 150,
    elevation: 20,
    borderBottomLeftRadius: 100,
    paddingTop: 15,
    paddingBottom: 15,
    paddingLeft: 15,
    paddingRight: 15,
  },
  nameText: {
    fontFamily: 'iransans',
    fontSize: 18,
    paddingTop: 3,
    textAlign: 'right',
    color: Colors.white,
    width: 170,
  },
  codeText: {
    fontFamily: 'bnazanin',
    fontSize: 18,
    paddingTop: 3,
    textAlign: 'left',
    color: Colors.white,
    width: 150,
  },
  userIcon: {
    width: 30,
    height: 30,
    marginRight: 10,
    marginLeft: 5,
  },
  stats: {
    flexDirection: 'row',
    top: 50,
    left: 20,
    position: 'absolute',
    elevation: 50,
    borderRadius: 10,
    backgroundColor: Colors.white,
    width: '90%',
  },
  statsItem: {
    paddingBottom: 10,
    paddingTop: 10,
    marginTop: 25,
    marginBottom: 25,
    marginLeft: 5,
    marginRight: 10,
    width: '30%',
  },
  statsItemTextTop: {
    fontFamily: 'bnazanin',
    fontSize: 20,
    textAlign: 'center',
    color: Colors.primary,
  },
  statsItemTextBottom: {
    fontFamily: 'bnazanin',
    fontSize: 20,
    textAlign: 'center',
    color: Colors.gray,
  },
  bottom: {
    backgroundColor: Colors.transparent,
  },
  navItem: {
    backgroundColor: Colors.transparent,
    height: 80,
    flexDirection: 'row',
    top: 50,
    left: 20,
    borderRadius: 4,
    width: '90%',
  },
});
export default ProfileScreen;
