<template>
  <form id="myForm" class="needs-validation" novalidate @submit.prevent="TryReset">
    <div class="container">
      <Toast position="top-center" />

      <div class="row">
        <div class="col">
          <div class="form-floating">
            <input v-model="this.User.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password" >
            <label for="floatingPassword">Password</label>
            <div v-if="errorFromApi.Password" class="invalid-feedback">
              <div v-for="(item, index) in errorFromApi.Password" :key="index">
                - {{ item }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col">
          <div class="form-floating">
            <input v-model="this.User.NewPassword" type="password" class="form-control" id="floatingNewPassword" placeholder="New Password" >
            <label for="floatingNewPassword">New Password</label>
            <div v-if="errorFromApi.NewPassword" class="invalid-feedback">
              <div v-for="(item, index) in errorFromApi.NewPassword" :key="index">
                - {{ item }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col">
          <div class="form-floating">
            <input v-model="this.User.NewConfirmedPassword" type="password" class="form-control" id="floatingNewConfirmedPassword" placeholder="Confirm New Password" >
            <label for="floatingNewConfirmedPassword">Confirmed New Password</label>
            <div v-if="errorFromApi.NewConfirmedPassword" class="invalid-feedback">
              <div v-for="(item, index) in errorFromApi.NewConfirmedPassword" :key="index">
                - {{ item }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col">
          <button type="submit" class="btn btn-primary">Reset</button>
        </div>
      </div>
    </div>
  </form>
</template>

  
  
  <script>
  import {ResetPassword} from '../../services/ApiCommunication'

  export default {
    name: "ResetPassword",
    data() {
      return {
        User:{
          Password: null,
          NewPassword: null,
          NewConfirmedPassword: null
        },
        errorFromApi:{
          Password: null,
          NewPassword: null,
          NewConfirmedPassword: null
        }
        
      }
    },
    methods: {
      isEmpty: function(value){
        return (value == null || (typeof value === "string" && value.trim().length === 0));
      },
      ConverString: function(value){
        console.log(value.length)
      if (value.length > 1) {
          let outValue = "";
          value.forEach(element => {
              outValue += "\n" + element + "";
          });
          return outValue; 
      } else {
          return value[0];
      }
  },
      async TryReset() {
            var PasswordInput = document.getElementById("floatingPassword");
            var NewPasswordInput = document.getElementById("floatingNewPassword");
            var NewConfirmedPasswordInput = document.getElementById("floatingNewConfirmedPassword");

  
        if(this.isEmpty(this.User.Password) || this.isEmpty(this.User.NewPassword) || this.isEmpty(this.User.NewConfirmedPassword) || this.User.NewConfirmedPassword !== this.User.NewPassword)
        {
          if(this.isEmpty(this.User.Password))
            PasswordInput.classList.add("is-invalid")
          else
            PasswordInput.classList.replace("is-invalid","is-valid")
          
          if(this.isEmpty(this.User.NewPassword))
            NewPasswordInput.classList.add("is-invalid")
          else
            NewPasswordInput.classList.replace("is-invalid","is-valid")

          if(this.isEmpty(this.User.NewConfirmedPassword))
            NewConfirmedPasswordInput.classList.add("is-invalid")
          else
            NewConfirmedPasswordInput.classList.replace("is-invalid","is-valid")
        }
        else{
        await ResetPassword(this.User)
        .then(response => {console.log(response)
          if(response.status ===200){

            NewConfirmedPasswordInput.classList.replace("is-invalid","is-valid")
            NewPasswordInput.classList.replace("is-invalid","is-valid")
            PasswordInput.classList.replace("is-invalid","is-valid")
  
            this.$toast.add({ severity: 'success', summary: 'Password Reset correctly', life: 3000 });
  
            this.$router.push("/UserInfo")          
          }
        })
        .catch(error => {
          if(error.response && error.response.status === 400){
            console.log(error.response.data.errors)
            
              if(!this.isEmpty(error.response.data.errors.Password)){
                this.errorFromApi.Password = error.response.data.errors.Password,
                PasswordInput.classList.add("is-invalid")
              }else
                PasswordInput.classList.replace("is-invalid","is-valid")

              if(!this.isEmpty(error.response.data.errors.NewPassword)){
                this.errorFromApi.NewPassword = error.response.data.errors.NewPassword,
                NewPasswordInput.classList.add("is-invalid")
              }else
                NewPasswordInput.classList.replace("is-invalid","is-valid")

              if(!this.isEmpty(error.response.data.errors.NewConfirmedPassword)){
                this.errorFromApi.NewConfirmedPassword = error.response.data.errors.NewConfirmedPassword,
                NewConfirmedPasswordInput.classList.add("is-invalid")
              }else
                NewConfirmedPasswordInput.classList.replace("is-invalid","is-valid")
              
          }
        })
      }
      }
    }
  }
  </script>
  
  
  
  
  
  <style scoped>
  .btn{
    margin-left: 40%;
  }
  
  .container {
    margin: auto;
    width: 50%;
  }
  
  .form-floating {
    width: 20vw;
    margin-top:4vh;
    margin-left: 2vw;
  }
  
  .d-flex {
    display: flex;
  }
  
  .invalid-feedback{
    font-size: 65%;
    text-align: left;
  }
  
  </style>
  