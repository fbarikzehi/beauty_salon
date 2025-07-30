import React from 'react';

import Icon from 'react-native-vector-icons/Ionicons';

import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator} from '@react-navigation/stack';
import {createMaterialBottomTabNavigator} from '@react-navigation/material-bottom-tabs';

import SplashScreen from './screens/SplashScreen';
import LoginScreen from './screens/LoginScreen';
import HomeScreen from './screens/HomeScreen';
import ProfileScreen from './screens/ProfileScreen';

const Stack = createStackNavigator();
const BottomTabNavigator = createMaterialBottomTabNavigator();

const App = () => {
  let initialRouteName = 'Splash';

  const createBottomTabs = () => {
    return (
      <BottomTabNavigator.Navigator
        backBehavior="none"
        shifting={true}
        activeColor="#f0edf6"
        inactiveColor="#3e2465"
        barStyle={{backgroundColor: '#7203EA'}}>
        <BottomTabNavigator.Screen
          name="HomeTabScreen"
          component={HomeScreen}
          options={{
            tabBarLabel: 'نوبت ها',
            tabBarIcon: () => (
              <Icon
                style={[{color: 'white'}]}
                size={25}
                name={'grid-outline'}
              />
            ),
            tabBarColor: '#00B1CA',
          }}
        />
        <BottomTabNavigator.Screen
          name="ProfileTabScreen"
          component={ProfileScreen}
          options={{
            tabBarLabel: 'پروفایل',
            tabBarIcon: () => (
              <Icon style={[{color: 'white'}]} size={25} name={'md-person'} />
            ),
            tabBarColor: '#7203EA',
          }}
        />
      </BottomTabNavigator.Navigator>
    );
  };

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName={initialRouteName}>
        <Stack.Screen
          name="Splash"
          component={SplashScreen}
          options={{headerShown: false}}
        />
        <Stack.Screen
          name="Login"
          component={LoginScreen}
          options={{headerShown: false}}
        />
        <Stack.Screen
          name="Home"
          children={createBottomTabs}
          options={{headerShown: false, headerTitleAlign: 'center'}}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default App;
