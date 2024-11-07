import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/home/Home.vue'
import Login from '../views/Login/Login.vue'
import Register from '../views/Register/Register.vue'
import AccountDetails from '@/views/user/AccountDetails.vue'
import OpeningHoursForm from '@/views/user/OpeningHours/OpeningHoursForm.vue'
import ReceiveMessage from '@/views/Messages/ReceiveMessage.vue'
import MessageResponse from '@/views/Messages/MessageResponse.vue'
import Accidents from '@/views/Accident/Accidents.vue'
import AccidentFullInfo from '@/views/Accident/AccidentFullInfo.vue'
import AddRepairHistory from '@/views/RepairHistory/AddRepairHistory.vue'
import RepairHistoryDetails from '@/views/RepairHistory/RepairHistoryDetails.vue'
const routes = [ 
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/Register',
    name: 'Register',
    component: Register
  },
  {
    path: '/Login',
    name: 'Login',
    component: Login
  },
  {
    path: '/Account',
    name: 'Account',
    component: AccountDetails
  },
  {
    path: '/Hours',
    name: 'Hours',
    component: OpeningHoursForm
  },
  {
    path: '/ReceiveMessage',
    name: 'ReceiveMessage',
    component: ReceiveMessage
  },
  {
    path: '/MessageResponse/:id',
    name: 'MessageResponse',
    component: MessageResponse,
    props: true 
  },
  {
    path: '/Accidents',
    name: 'Accidents',
    component: Accidents
  },
  {
    path: '/AccidentFullInfo/:id',
    name: 'AccidentFullInfo',
    component: AccidentFullInfo,
    props: true 
  },
  {
    path: '/AddRepairHistory/:id/:action',
    name: 'AddRepairHistory',
    component: AddRepairHistory,
    props: true 
  }
  ,
  {
    path: '/RepairHistoryDetails/:id',
    name: 'RepairHistoryDetails',
    component: RepairHistoryDetails,
    props: true 
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})


export default router