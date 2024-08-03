<template>

  <div>
    <Toast position="top-center" />

    <div class="UserInformation">
      <p><strong>Imię: </strong> {{ this.User.name }}</p>
      <p><strong>Nazwisko: </strong> {{ this.User.surname }}</p>
      <p><strong>Email: </strong> {{ this.User.email }}</p>
      <p><strong>Numer telefonu: </strong> {{ this.User.phoneNumber !== null ? getPhoneNumber(this.User.phoneNumber) : ""}}</p>
      <p><strong>Data urodzenia: </strong> {{ this.User.dateOfBierh }}</p>
    </div>

    <div class="car-section">
    <button type="button" @click="AddCar" class="btn btn-outline-info btn-lg addcarButton">Add Car</button>
    <div class="cardSection">
      <div v-for="(car, index) in User.cars" :key="index" class="card" :class="{'even': index % 2 === 0, 'odd': index % 2 !== 0}" :style="[getBorderColor(car.color)]">
        <img class="card-img-top" src="car-photo.jpg" alt="Card image cap">
        <div class="card-body">
          <h5 class="card-title">{{ car.brand }} {{ car.model }}</h5>
          <p class="card-text">VIN: {{ car.vin }}</p>
          <p class="card-text">Car Type: {{ car.carType }}</p>
          <a href="#" class="btn" :style="getButtonStyle(car.color)" @click="CarDetails(car.vin)">Car Details</a>
        </div>
      </div>
    </div>
  </div>

        <Dialog v-model:visible="visible" modal header="Change your password" :style="{ width: '27rem' }">

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


      <div class="functional-buttons">
        <button type="submit" @click="visible = true" class="btn btn-warning">Reset Password</button>
      </div>


    </div>
</template>
    
<script>
import { fetchUserData, ResetPassword } from '@/services/UserApiCommunication.js';
import router from '@/router';


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
        cars:null,
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
      this.User.cars = this.response.cars
      console.log(this.User.cars)
    },
    ResetPassword(){
      this.displayFormula = !this.displayFormula
    },
    AddCar(){
      router.push("/CarCreator")
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
      },
    getCarColor(color) {
      return `rgb(${color.red}, ${color.green}, ${color.blue})`;
    },
    getBrightness(color) {
      return (color.red * 299 + color.green * 587 + color.blue * 114) / 1000;
    },
    getButtonStyle(color) {
      const backgroundColor = this.getCarColor(color);
      const brightness = this.getBrightness(color);
      const textColor = brightness < 128 ? 'white' : 'black';
      return {
        backgroundColor: backgroundColor,
        color: textColor
      };
    },
    getBorderColor(color) {
      return {
        border: `2px solid rgb(${color.red}, ${color.green}, ${color.blue})`,
      };
    },
    getCardAlignment(index) {
      return {
        float: index % 2 === 0 ? 'left' : 'right'
      };
    },
    getPhoneNumber(phoneNumber) {
    let phoneStr = phoneNumber.toString();

    let formatted = phoneStr.replace(/(\d{3})(?=\d)/g, '$1 ');

    return `(+48) ${formatted}`;
  },
  CarDetails(vin){
    router.push(`/CarDetails/${vin}`)
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
    width: fit-content;
  }
  .UserInformation p {
    padding-top: 1.5%;
    font-size: 150%;
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
  .car-section{
    position: absolute;
    top: 12vh;
    right: 3vw;
    width: 35vw;
    height: fit-content;
  }

  .addcarButton{
    width: 35vw;
    letter-spacing: 4px;
    color: white;
  }

  .cardSection {
  display: flex;
  flex-wrap: wrap;
}

.card {
  width: 15.5vw;
  margin-top: 1rem;
  border-radius: 5%;
}

.card.even {
  align-self: flex-start;
  margin-right: auto;
}

.card.odd {
  align-self: flex-start;
  margin-left: auto;
}

.functional-buttons{
  text-align: left;
  padding-left: 3vw;
  padding-top: 2vh;
}

  
</style>