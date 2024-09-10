<template>
  <div class="mainForm">
    <form id="myForm" class="needs-validation my-form-validation" novalidate @submit.prevent="tryLogin">
      <div class="container">
        <CustomInput v-model="Email" label="Email" type="email" :error="emailError" :width='15'/>
        <CustomInput v-model="Password" label="Password" type="password" :error="passwordError" :width='15'/>
        <button type="submit" class="btn btn-primary">Login</button>
      </div>
    </form>
  </div>
</template>

<script>
import CustomInput from '../../components/Form/CustomInput.vue';
import LoginForm from '../../Models/AuthModels/LoginForm';
import {Login} from '../../ApiCommunication/Auth'

export default {
  name: 'Login',
  components: { CustomInput },
  data() {
    return {
        Email: '',
        Password: '',
        emailError: null,
        passwordError: null
    };
  },
  methods: {
    async tryLogin() {
      try{
        await Login(new LoginForm(this.Email, this.Password))
      }catch(ex){
        console.log(ex)
      }
    },
  },
};
</script>

<style scoped>
@import './Login.css';
</style>
