import React from 'react';
import {StyleSheet, View, Text, Image, TouchableHighlight} from 'react-native';
import Http from '../constants/EndPointUrls';
import Icon from 'react-native-vector-icons/Ionicons';
import AppointmentService from '../services/AppointmentService';

const AppointmentCard = (props) => {
  const onDonePress = async (appointmentId, appointmentServiceId) => {
    props.onUpdateActivityIndicator(true);
    let done = await AppointmentService.Done(
      appointmentId,
      appointmentServiceId,
    );
    if (done.isSuccess) {
      props.onDoneServiceHandler(appointmentId, appointmentServiceId);
    }
    props.onUpdateActivityIndicator(false);
    props.messageHandler(done.message);
  };

  return (
    <View style={styles.container}>
      <View style={styles.card}>
        <View style={styles.ellipseRow}>
          <Image
            style={styles.avatar}
            source={{uri: Http.baseSiteUrl + props.service.customerAvatarUrl}}
            defaultSource={require('../assets/images/avatar.png')}
          />
          <View style={styles.topLeft}>
            <Text style={styles.topLeftTime}>
              زمان مراجعه: {props.service.time}
            </Text>
            <Text style={styles.topLeftPrice}>
              {props.service.servicePrice} تومان
            </Text>
          </View>
          <View style={styles.topRight}>
            <Text style={styles.serviceName}>{props.service.serviceTile}</Text>
            <Text style={styles.customerFullName}>
              {props.service.customerFullName}
            </Text>
          </View>
        </View>
        <View style={[styles.ellipseRow, styles.actionRow]}>
          <View style={styles.actions}>
            <TouchableHighlight
              onPress={() =>
                onDonePress(
                  props.service.appointmentId,
                  props.service.appointmentServiceId,
                )
              }>
              <View style={styles.actionBtn}>
                <Icon
                  style={styles.actionDoneBtn}
                  size={20}
                  name={'checkmark-outline'}
                />
              </View>
            </TouchableHighlight>
          </View>
        </View>
      </View>
    </View>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  card: {
    width: '95%',
    height: 140,
    backgroundColor: '#fff',
    borderRadius: 6,
    shadowColor: '#fff',
    shadowOffset: {
      width: 3,
      height: 3,
    },
    elevation: 0,
    shadowOpacity: 0.01,
    shadowRadius: 0,
    marginTop: 10,
    marginBottom: 5,
    alignSelf: 'center',
  },
  avatar: {
    width: 55,
    height: 55,
    borderRadius: 100,
    padding: 10,
    backgroundColor: '#CED3DC',
  },
  topLeftTime: {
    fontFamily: 'bnazanin',
    fontSize: 17,
    color: '#121212',
  },
  topLeftPrice: {
    fontFamily: 'bnazanin',
    fontSize: 17,
    color: 'rgba(138,138,138,1)',
    marginTop: 5,
    textAlign: 'center',
  },
  topLeft: {
    width: 102,
    marginLeft: 13,
    marginBottom: 12,
  },
  topRight: {
    width: '50%',
    marginRight: 13,
    marginBottom: 12,
  },
  serviceName: {
    // fontFamily: "roboto-regular",
    color: '#121212',
    textAlign: 'right',
  },
  ellipseRow: {
    height: 51,
    flexDirection: 'row',
    marginTop: 18,
    marginLeft: 0,
    marginRight: 0,
    paddingLeft: 10,
  },
  customerName: {
    // fontFamily: "roboto-regular",
    color: 'rgba(138,138,138,1)',
    marginTop: 9,
    marginLeft: 11,
  },
  actionRow: {
    borderTopColor: '#E0DDCF',
    borderTopWidth: 0.3,
    width: '100%',
    paddingTop: 5,
  },
  actions: {
    marginTop: 0,
    marginLeft: 330,
  },
  actionBtn: {
    backgroundColor: '#03b073',
    borderRadius: 3,
    padding: 10,
    marginRight: 0,
    alignSelf: 'flex-end',
  },
  actionDoneBtn: {
    color: '#ffffff',
  },
});

export default AppointmentCard;
