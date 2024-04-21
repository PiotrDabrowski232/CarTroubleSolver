<template>
  
    <div class="UserInformation">
        <P><strong>Name: </strong> {{ this.User.name }}</P>
        <P><strong>Surname: </strong> {{ this.User.surname }}</P>
        <P><strong>Email: </strong> {{ this.User.email }}</P>
        <P><strong>Phone Number: </strong> {{ this.User.phoneNumber }}</P>
        <P><strong>Date Of Birth: </strong> {{ this.User.dateOfBierh }}</P>
    </div>

</template>
    
<script>
import { fetchUserData } from '@/services/ApiCommunication';

    export default {
  name: 'UserInformation',
  data(){
    return {
      response:null,
      User:{
        name:null,
        surname:null,
        email:null,
        phoneNumber:null,
        dateOfBierh:null,
      }
    }
  },
  mounted(){
      this.method()
  },
  methods:{
    async method(){
      this.response = await fetchUserData()
      this.User.surname = this.response.surname,
      this.User.name = this.response.name,
      this.User.email = this.response.email,
      this.User.phoneNumber = this.response.phoneNumber,
      this.User.dateOfBierh = this.response.dateOfBirth
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
    padding-top: 2.8%;
    padding-left: 5%;
  }
  .UserInformation p {
    padding-top: 1.5%;
    font-size: 140%;
  }
</style>