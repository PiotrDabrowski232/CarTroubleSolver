<template>
  

  <form id="myForm" class="needs-validation my-form-validation" novalidate @submit.prevent="tryLogin">
  
    <Toast position="top-center" />
    <div class="container">
     
        <div class="form-floating">
          <input v-model="this.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com" required>
          <label for="floatingEmail">Email address</label>
        </div>
  
        <div class="form-floating">
          <input v-model="this.Password" type="password" class="form-control" id="floatingPassword" placeholder="" >
          <label for="floatingPassword">User Name</label>
        </div>

          <button type="submit" class="btn btn-primary login-button">Login</button>
    </div>  
  </form>
  
  </template>
  

  
<script>
import axios from 'axios';

  export default {
name: 'LoginPage',
data(){
  return {
        Email:null,
        Password: null,
  }
},
methods: {
    isEmpty: function(value){
      return (value == null || (typeof value === "string" && value.trim().length === 0));
    },
    async tryLogin(){

        var EmailInput = document.getElementById("floatingEmail");
        var PasswordInput = document.getElementById("floatingPassword");

      if(this.isEmpty(this.Email) || this.isEmpty(this.Password)){
        if(this.isEmpty(this.Email))
          EmailInput.classList.add("is-invalid")
        else
          EmailInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.Password))
          PasswordInput.classList.add("is-invalid")
        else
          PasswordInput.classList.replace("is-invalid","is-valid")
      }else{
      await axios.post(`http://localhost:5113/Login`, {
        Email: this.Email,
        Password: this.Password
      })
      .then(response => {
        console.log(response.data)
        this.$toast.add({ severity: 'success', summary: 'Login Successfully', life: 3000 });
          //this.$router.push("/Register")
          localStorage.setItem('token', response.data)
      })
      .catch(error => {
        if(error.response && error.response.status === 400){
          console.log("error")
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