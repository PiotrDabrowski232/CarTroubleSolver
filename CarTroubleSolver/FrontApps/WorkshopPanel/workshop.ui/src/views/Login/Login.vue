<template>
  <div class="mainForm">
    <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
    <form id="myForm" class="needs-validation my-form-validation" novalidate @submit.prevent="tryLogin">
      <div class="container">
        <CustomInput v-model="Email" label="Email" type="email" :width='15'/>
        <CustomInput v-model="Password" label="Password" type="password" :width='15'/>
        <button type="submit" class="btn btn-primary">Login</button>
      </div>
    </form>
  </div>
</template>

<script>
import CustomInput from '../../components/Form/CustomInput.vue';
import LoginForm from '../../Models/AuthModels/LoginForm';
import {Login} from '../../ApiCommunication/Auth'
import Toast from '@/components/Toast.vue';
import router from '@/router';

export default {
  name: 'Login',
  components: { CustomInput,Toast },
  data() {
    return {
        Email: '',
        Password: '',
        toastMsg : '',
        showToast : false,
        toastColor : ''
    };
  },
  methods: {
    async tryLogin() {
      try{
        await Login(new LoginForm(this.Email, this.Password))
        this.showToastMessage("Logged Successfully", "success")
        setTimeout(() => {
          router.push("/")
        }, 3000);
      }catch(ex){
        this.showToastMessage("Invalid email or password", "danger")
      }
    },
    showToastMessage(message, color) {
      this.toastMsg = message;
      this.showToast = true;
      this.toastColor = color;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
  },
};
</script>

<style scoped>
@import './Login.css';
</style>
