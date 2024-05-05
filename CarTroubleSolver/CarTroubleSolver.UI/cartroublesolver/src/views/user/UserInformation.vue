<template>

  <div>
    <Toast position="top-center" />

    <div class="UserInformation">
      <p><strong>Imię: </strong> {{ this.User.name }}</p>
      <p><strong>Nazwisko: </strong> {{ this.User.surname }}</p>
      <p><strong>Email: </strong> {{ this.User.email }}</p>
      <p><strong>Numer telefonu: </strong> {{ this.User.phoneNumber }}</p>
      <p><strong>Data urodzenia: </strong> {{ this.User.dateOfBierh }}</p>
    </div>
        <Dialog v-model:visible="visible" modal header="Change your password" :style="{ width: '25rem' }">

          <form id="myForm" class="needs-validation" novalidate @submit.prevent="TryReset">

            <div class="form-floating">
              <input v-model="this.ResetUser.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password" >
              <label for="floatingPassword">Password</label>

              <div v-if="errorFromApi.Password" class="invalid-feedback">
                <div v-for="(item, index) in errorFromApi.Password" :key="index">
                  - {{ item }}
                </div>
              </div>
            </div>

            <div class="form-floating">
              <input v-model="this.ResetUser.NewPassword" type="password" class="form-control" id="floatingNewPassword" placeholder="New Password" >
              <label for="floatingNewPassword">New Password</label>

              <div v-if="errorFromApi.NewPassword" class="invalid-feedback">
                <div v-for="(item, index) in errorFromApi.NewPassword" :key="index">
                  - {{ item }}
                </div>
              </div>
            </div>

            <div class="form-floating">
              <input v-model="this.ResetUser.NewConfirmedPassword" type="password" class="form-control" id="floatingNewConfirmedPassword" placeholder="Confirm New Password" >
              <label for="floatingNewConfirmedPassword">Confirm New Password</label>

              <div v-if="errorFromApi.NewConfirmedPassword" class="invalid-feedback">
                <div v-for="(item, index) in errorFromApi.NewConfirmedPassword" :key="index">
                  - {{ item }}
                </div>
              </div>
            </div>

            <div class="flex justify-content-end gap-3 inline-flex">
                <button type="button" @click="visible = false" class="btn btn-danger dialogButton">Cancel</button>
                <button type="submit" class="btn btn-success dialogButton">Save</button>
            </div>

          </form>
          
        </Dialog>

        <button type="submit" @click="visible = true" class="btn btn-outline-info">Info</button>
        <router-link class="router-link" to="/CarCreator" exact-path>Car</router-link>

    </div>
</template>
    
<script>
import { fetchUserData, ResetPassword } from '@/services/UserApiCommunication.js';


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
        dateOfBierh:null
      },
      ResetUser:{
          Password: null,
          NewPassword: null,
          NewConfirmedPassword: null
        },
        errorFromApi:{
          Password: null,
          NewPassword: null,
          NewConfirmedPassword: null
        },
        visible: false,
    }
  },
  mounted(){
      this.fetchData()
  },
  methods:{
      isEmpty: function(value){
        return (value == null || (typeof value === "string" && value.trim().length === 0));
      },
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
    },
    async TryReset() {
            var PasswordInput = document.getElementById("floatingPassword");
            var NewPasswordInput = document.getElementById("floatingNewPassword");
            var NewConfirmedPasswordInput = document.getElementById("floatingNewConfirmedPassword");
           
        await ResetPassword(this.ResetUser)
        .then(response => {
          console.log(response)
          if(response.status === 200){
            
            this.$toast.add({ severity: 'success', summary: 'Password Reset correctly', life: 3000 });
            this.visible=false
            this.ResetUser.Password = "";
            this.ResetUser.NewPassword = "";
            this.ResetUser.NewConfirmedPassword = "";
          }
        })
        .catch(error => {
          if(error.response && error.response.status === 400){

            if(error.response.data.errors === undefined){
              this.$toast.add({ severity: 'error', summary: 'Incorrect Password Passed', life: 5000 });
                PasswordInput.classList.add("is-invalid")
                NewPasswordInput.classList.replace("is-invalid","is-valid")
                NewConfirmedPasswordInput.classList.replace("is-invalid","is-valid")

            }else{
              if(!this.isEmpty(error.response.data.errors.OldPassword)){
                this.errorFromApi.Password = error.response.data.errors.OldPassword,
                PasswordInput.classList.add("is-invalid")
              }else
                PasswordInput.classList.replace("is-invalid","is-valid")

              if(!this.isEmpty(error.response.data.errors.NewPassword)){
                this.errorFromApi.NewPassword = error.response.data.errors.NewPassword,
                NewPasswordInput.classList.add("is-invalid")
              }else
                NewPasswordInput.classList.replace("is-invalid","is-valid")

              if(!this.isEmpty(error.response.data.errors.ConfirmedNewPassword)){
                this.errorFromApi.NewConfirmedPassword = error.response.data.errors.ConfirmedNewPassword,
                NewConfirmedPasswordInput.classList.add("is-invalid")
              }else
                NewConfirmedPasswordInput.classList.replace("is-invalid","is-valid")
              }
          }
        })
      }
      }
  }

</script>
  
  
    
<style>
.dialogButton{
  margin-top:2.4vh;
}
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
  .btn-success{
    margin-left: 50%;
  }
</style>