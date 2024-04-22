<template>
  <div>
    <div class="UserInformation">
      <p><strong>Imię: </strong> {{ this.User.name }}</p>
      <p><strong>Nazwisko: </strong> {{ this.User.surname }}</p>
      <p><strong>Email: </strong> {{ this.User.email }}</p>
      <p><strong>Numer telefonu: </strong> {{ this.User.phoneNumber }}</p>
      <p><strong>Data urodzenia: </strong> {{ this.User.dateOfBierh }}</p>
    </div>
    <button type="button" @click="ResetPassword" id="buttoner" class="btn btn-primary">Zmień hasło</button>
    <transition name="fade">
      <ResetPassword v-if="displayFormula"/>
    </transition>
  </div>
</template>
    
<script>
import { fetchUserData } from '@/services/ApiCommunication';
import ResetPassword from './ResetPassword.vue';

    export default {
  name: 'UserInformation',
  components:{
    ResetPassword
  },
  data(){
    return {
      response:null,
      User:{
        name:null,
        surname:null,
        email:null,
        phoneNumber:null,
        dateOfBierh:null,
      },
      displayFormula: false,
    }
  },
  mounted(){
      this.fetchData()
  },
  methods:{
    async fetchData(){
      this.response = await fetchUserData()
      this.User.surname = this.response.surname,
      this.User.name = this.response.name,
      this.User.email = this.response.email,
      this.User.phoneNumber = this.response.phoneNumber,
      this.User.dateOfBierh = this.response.dateOfBirth
    },
    ResetPassword(){
      this.displayFormula = !this.displayFormula
    }
  }
}
</script>
  
  
    
<style>
  .MainContent{
    padding-top: 5vh;
  }
  .UserInformation{
    text-align: left;
    padding-top: 2%;
    padding-left: 3%;
  }
  .UserInformation p {
    padding-top: 1.5%;
    font-size: 120%;
  }
  #buttoner{
    margin-right: 87.6%;
    margin-top: 1%;
  }
  .fade-enter-active, .fade-leave-active {
    transition: opacity 0.5s;
  }
  .fade-enter, .fade-leave-to {
    opacity: 0;
  }
  .fade-enter {
    transition-delay: 0.5s;
  }
</style>