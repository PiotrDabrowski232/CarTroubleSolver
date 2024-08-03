<template>
  

  <form id="myForm" class="needs-validation my-form-validation" novalidate @submit.prevent="tryLogin">
  
    <Toast position="top-center" />
    <div class="container">
     
        <div class="form-floating">
          <input v-model="this.user.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com" required>
          <label for="floatingEmail">Email address</label>
        </div>
  
        <div class="form-floating">
          <input v-model="this.user.Password" type="password" class="form-control" id="floatingPassword" placeholder="" >
          <label for="floatingPassword">User Name</label>
        </div>

          <button type="submit" class="btn btn-primary login-button">Login</button>
    </div>  
  </form>
  
</template>
  

  
<script>
import {LoginUser} from '../services/UserApiCommunication'

  export default {
name: 'LoginPage',
data(){
  return {
      user:{
        Email:null,
        Password: null,
      }
  }
},
methods: {
    isEmpty: function(value){
      return (value == null || (typeof value === "string" && value.trim().length === 0));
    },
    async tryLogin(){

        var EmailInput = document.getElementById("floatingEmail");
        var PasswordInput = document.getElementById("floatingPassword");

      if(this.isEmpty(this.user.Email) || this.isEmpty(this.user.Password)){
        if(this.isEmpty(this.user.Email))
          EmailInput.classList.add("is-invalid")
        else
          EmailInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.user.Password))
          PasswordInput.classList.add("is-invalid")
        else
          PasswordInput.classList.replace("is-invalid","is-valid")
      }else{
        LoginUser(this.user)
        .then(() => {
        this.$toast.add({ severity: 'success', summary: 'Login Successfully', life: 3000 });
          setTimeout(() => {
            this.$router.push("/")
          }, 3000);
      })
      .catch(error => {
        if(error.response && error.response.status === 400){
          this.$toast.add({ severity: 'error', summary: 'Email or password invalid', life: 3000 });
        }
      })
    }
    }
  }
}
</script>


  
<style>
.login-button{
  margin-top: 8vh;
  margin-right: 12vw;
}
.my-form-validation{
  padding-left: 40%;
  padding-top: 12%;
}
.form-floating{
  width: 20vw;
  margin-top: 4.5vh;
}
</style>