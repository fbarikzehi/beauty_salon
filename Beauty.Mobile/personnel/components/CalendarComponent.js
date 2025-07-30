import React from 'react';
import {StyleSheet, View, Text} from 'react-native';

const CalendarComponent = () => {
  return (
    <View style={styles.container}>
      <Text style={{textAlign: 'center', fontFamily: 'bnazanin', fontSize: 22}}>
        اینجا قراره تقویم باشه
      </Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});
export default CalendarComponent;
