<template>
  
<form id="myForm" class="needs-validation" novalidate @submit.prevent="postPost">
  

  <div class="container">
    <Toast position="top-center" />
      <div class="d-flex">

        <div class="form-floating">
          <input v-model="User.Name" type="text" class="form-control" id="floatingName" placeholder="Name">
          <label for="floatingName">Name</label>
          <div v-if="errorFromApi.Name" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.Name" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.Surname" type="text" class="form-control" id="floatingSurname" placeholder="Surname" >
          <label for="floatingSurname">Surname</label>
          <div v-if="errorFromApi.Surname" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.Surname" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>
        
      </div>

      <div class="form-floating">
        <input v-model="User.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com" >
        <label for="floatingEmail">Email address</label>
   
        <div v-if="errorFromApi.Email" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.Email" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

      <div class="form-floating">
        <input v-model="User.UserName" type="text" class="form-control" id="floatingUserName" placeholder="" >
        <label for="floatingUserName">User Name</label>
          <div v-if="errorFromApi.UserName" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.UserName" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password" >
          <label for="floatingPassword">Password</label>
          <div v-if="errorFromApi.Password" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.Password" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.ConfirmedPassword" type="password" class="form-control" id="floatingConfirmedPassword" placeholder="Confirm password" >
          <label for="floatingConfirmedPassword">Confirm Password</label>
          <div v-if="errorFromApi.ConfirmedPassword" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.ConfirmedPassword" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>
      </div>

      <div class="form-floating">
          <input v-model="User.DateOfBirth" type="date" class="form-control" id="DateOfBirth" placeholder="DateOfBirth" >
          <label for="Date Of Birth">Date Of Birth</label>
          <div v-if="errorFromApi.DateOfBirth" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.DateOfBirth" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>

      <div class="form-floating">
        <input v-model="User.PhoneNumber"  type="number" :minlength="9" :maxlength="9" class="form-control" id="PhoneNumber" placeholder="PhoneNumber" >
        <label for="PhoneNumber">Phone Number</label>
          <div v-if="errorFromApi.PhoneNumber" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.PhoneNumber" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

        <button type="submit" class="btn btn-primary">Create Account</button>
  </div>  
</form>

</template>


<script>
import router from '@/router';
import axios from 'axios';

export default {
  name: "RegisterPage",
  data() {
    return {
      User:{
        UserName:null,
        Name: null,
        Surname: null,
        Email:null,
        Password: null,
        PhoneNumber:null,
        DateOfBirth: null,
        ConfirmedPassword:null,
      },
      errorFromApi:{
        UserName:null,
        Name: null,
        Surname: null,
        Email:null,
        Password: null,
        PhoneNumber:null,
        DateOfBirth: null,
        ConfirmedPassword:null,
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
    async postPost() {
          var NameInput = document.getElementById("floatingName");
          var SurnameInput = document.getElementById("floatingSurname");
          var UserNameInput = document.getElementById("floatingSurname");
          var EmailInput = document.getElementById("floatingEmail");
          var PasswordInput = document.getElementById("floatingPassword");
          var PhoneNumberInput = document.getElementById("PhoneNumber");
          var DateOfBirthInput = document.getElementById("DateOfBirth");
          var ConfirmedPasswordInput = document.getElementById("floatingConfirmedPassword");

      if(this.isEmpty(this.User.UserName) || this.isEmpty(this.User.Name) || this.isEmpty(this.User.Surname) || this.isEmpty(this.User.Email) || this.isEmpty(this.User.Password) || this.isEmpty(this.User.PhoneNumber) || this.isEmpty(this.User.ConfirmedPassword) || this.isEmpty(this.User.DateOfBirth))
      {
        if(this.isEmpty(this.User.UserName))
          UserNameInput.classList.add("is-invalid")
        else
          UserNameInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.Name))
          NameInput.classList.add("is-invalid")
        else
          NameInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.Surname))
          SurnameInput.classList.add("is-invalid")
        else
          SurnameInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.Email))
          EmailInput.classList.add("is-invalid")
        else
          EmailInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.Password))
          PasswordInput.classList.add("is-invalid")
        else
          PasswordInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.PhoneNumber))
          PhoneNumberInput.classList.add("is-invalid")
        else
          PhoneNumberInput.classList.replace("is-invalid","is-valid")
        
        if(this.isEmpty(this.User.ConfirmedPassword))
          ConfirmedPasswordInput.classList.add("is-invalid")
        else
          ConfirmedPasswordInput.classList.replace("is-invalid","is-valid")

        if(this.isEmpty(this.User.DateOfBirth))
          DateOfBirthInput.classList.add("is-invalid")
        else
          DateOfBirthInput.classList.replace("is-invalid","is-valid")
        
      }
      else{
      await axios.post(`http://localhost:5113/Register`, {
        Name: this.User.Name,
        Surname: this.User.Surname,
        Email: this.User.Email,
        Password: this.User.Password,
        PasswordConfirmed: this.User.ConfirmedPassword,
        PhoneNumber: this.User.PhoneNumber,
        DateOfBirth: this.date
      })
      .then(response => {console.log(response)
        if(response.status ===200){
          UserNameInput.classList.replace("is-invalid","is-valid")
          NameInput.classList.replace("is-invalid","is-valid")
          EmailInput.classList.replace("is-invalid","is-valid")
          PasswordInput.classList.replace("is-invalid","is-valid")
          PhoneNumberInput.classList.replace("is-invalid","is-valid")
          DateOfBirthInput.classList.replace("is-invalid","is-valid")
          ConfirmedPasswordInput.classList.replace("is-invalid","is-valid")

          this.$toast.add({ severity: 'success', summary: 'Account created correctly', life: 3000 });

          router.push("/Login")
        }
      })
      .catch(error => {
        if(error.response && error.response.status === 400){
          console.log(error.response.data.errors)
          
            if(!this.isEmpty(error.response.data.errors.UserName)){
              this.errorFromApi.UserName = error.response.data.errors.UserName,
              UserNameInput.classList.add("is-invalid")
            }else
              UserNameInput.classList.replace("is-invalid","is-valid")

            if(!this.isEmpty(error.response.data.errors.Name)){
              this.errorFromApi.Name = error.response.data.errors.Name,
              NameInput.classList.add("is-invalid")
            }else
              NameInput.classList.replace("is-invalid","is-valid")
            
            if(!this.isEmpty(error.response.data.errors.Email)){
              this.errorFromApi.Email = error.response.data.errors.Email,
              EmailInput.classList.add("is-invalid")
            }else
              EmailInput.classList.replace("is-invalid","is-valid")
            
            if(!this.isEmpty(error.response.data.errors.Password)){
              this.errorFromApi.Password = error.response.data.errors.Password,
              PasswordInput.classList.add("is-invalid")
            }else
              PasswordInput.classList.replace("is-invalid","is-valid")
            
            if(!this.isEmpty(error.response.data.errors.PhoneNumber)){
              this.errorFromApi.PhoneNumber = error.response.data.errors.PhoneNumber,
              PhoneNumberInput.classList.add("is-invalid")
            }else
              PhoneNumberInput.classList.replace("is-invalid","is-valid")
            
            if(!this.isEmpty(error.response.data.errors.DateOfBirth)){
              this.errorFromApi.DateOfBirth = error.response.data.errors.DateOfBirth,
              DateOfBirthInput.classList.add("is-invalid")
            }else
              DateOfBirthInput.classList.replace("is-invalid","is-valid")
            
            if(!this.isEmpty(error.response.data.errors.PasswordConfirmed)){
              this.errorFromApi.ConfirmedPassword = error.response.data.errors.PasswordConfirmed
              ConfirmedPasswordInput.classList.add("is-invalid")
            }else
              ConfirmedPasswordInput.classList.replace("is-invalid","is-valid")
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
