<template>
  <div class="mainForm">
    <form id="myForm" class="needs-validation my-form-validation" novalidate @submit.prevent="tryLogin">
      <div class="container">
        <CustomInput v-model="email" label="email" type="email" :error="emailError"/>
        <button type="submit" class="btn btn-primary">Login</button>
      </div>
    </form>
    <p>{{ email }}</p>
  </div>
</template>

<script>
import CustomInput from '../../components/Form/CustomInput.vue';

export default {
  name: 'Login',
  components: { CustomInput },
  data() {
    return {
      email: '',
      password: '',
    };
  },
  methods: {
    tryLogin() {
      console.log('Email:', this.email);
      console.log('Password:', this.password);
    },
  },
};
</script>

<style scoped>
@import './Login.css';
</style>
