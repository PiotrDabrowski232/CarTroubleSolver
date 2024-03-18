<template>
<form id="myForm" class="needs-validation" novalidate @submit.prevent="postPost">
  <div class="container">
   
      <div class="d-flex">

        <div class="form-floating">
          <input v-model="User.Name" type="text" class="form-control"  id="floatingName" placeholder="Name" required>
          <label for="floatingName">Name</label>
          <div v-if="errorFromApi.Name" class="invalid-feedback">
            {{errorFromApi.Name}}
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.Surname" type="text" class="form-control" id="floatingSurname" placeholder="Surname" required>
          <label for="floatingSurname">Surname</label>
          <div v-if="errorFromApi.Surname" class="invalid-feedback">
            {{errorFromApi.Surname}}
          </div>
        </div>
        
      </div>

      <div class="form-floating">
        <input v-model="User.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com" required>
        <label for="floatingEmail">Email address</label>
   
        <div v-if="errorFromApi.Email" class="invalid-feedback">
          {{errorFromApi.Email}}
          </div>
      </div>

      <div class="form-floating">
        <input v-model="User.UserName" type="text" class="form-control" id="floatingUserName" placeholder="" required>
        <label for="floatingUserName">User Name</label>
          <div v-if="errorFromApi.UserName" class="invalid-feedback">
            {{errorFromApi.UserName}}
          </div>
      </div>

      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password" required>
          <label for="floatingPassword">Password</label>
          <div v-if="errorFromApi.Password" class="invalid-feedback">
            {{errorFromApi.Password}}
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.ConfirmedPassword" type="password" class="form-control" id="floatingConfirmedPassword" placeholder="Confirm password" required>
          <label for="floatingConfirmedPassword">Confirm Password</label>
          <div v-if="errorFromApi.ConfirmedPassword" class="invalid-feedback">
            {{errorFromApi.ConfirmedPassword}}
          </div>
        </div>
      </div>

      <div class="form-floating">
          <input v-model="User.DateOfBirth" type="date" class="form-control" id="DateOfBirth" placeholder="Date Of Birth" required>
          <label for="Date Of Birth">Date Of Birth</label>
          <div v-if="errorFromApi.DateOfBirth" class="invalid-feedback">
            {{errorFromApi.DateOfBirth}}
          </div>
        </div>

      <div class="form-floating">
        <input v-model="User.PhoneNumber"  type="number" :minlength="9" :maxlength="9" class="form-control" id="PhoneNumber" placeholder="PhoneNumber" required>
        <label for="PhoneNumber">Phone Number</label>
          <div v-if="errorFromApi.PhoneNumber" class="invalid-feedback">
            {{errorFromApi.PhoneNumber}}
          </div>
      </div>

        <button @click="postPost" type="submit" class="btn btn-primary mt-2">Create Account</button>
  </div>  
</form>

</template>




<script>
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
        Name: "some text",
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
    async postPost() {
      if(this.User.UserName==null || this.User.Name==null || this.User.Surname==null || this.User.Email==null || this.User.Password==null || this.User.PhoneNumber==null || this.User.ConfirmedPassword==null)
      {
        console.log("empty values")
        console.log(this.User)
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
      .then(response => {console.log(response)})
      .catch(error => {
        if(error.response && error.response.status === 400){

          var NameInput = document.getElementById("floatingName");
          var UserNameInput = document.getElementById("floatingSurname");
          var EmailInput = document.getElementById("floatingEmail");
          var PasswordInput = document.getElementById("floatingPassword");
          var PhoneNumberInput = document.getElementById("PhoneNumber");
          var DateOfBirthInput = document.getElementById("DateOfBirth");
          var ConfirmedPasswordInput = document.getElementById("floatingConfirmedPassword");

            this.errorFromApi.UserName = error.response.data.errors.UserName,
            this.errorFromApi.Name = error.response.data.errors.Name,
            this.errorFromApi.Email = error.response.data.errors.Email,
            this.errorFromApi.Password = error.response.data.errors.Password,
            this.errorFromApi.PhoneNumber = error.response.data.errors.PhoneNumber,
            this.errorFromApi.DateOfBirth = error.response.data.errors.DateOfBirth,
            this.errorFromApi.ConfirmedPassword = error.response.data.errors.ConfirmedPassword
            console.log(error.response.data.errors)

            if(this.errorFromApi.UserName != null &&  this.errorFromApi.UserName != "")
              UserNameInput.classList.add("is-invalid")
            else
              UserNameInput.classList.add("is-valid")

            if(this.errorFromApi.Name != null &&  this.errorFromApi.Name != "")
              NameInput.classList.add("is-invalid")
            else
              NameInput.classList.add("is-valid")
            
            if(this.errorFromApi.Email != null &&  this.errorFromApi.Email != "")
              EmailInput.classList.add("is-invalid")
            else
              EmailInput.classList.add("is-valid")
            
            if(this.errorFromApi.Password != null &&  this.errorFromApi.Password != "")
              PasswordInput.classList.add("is-invalid")
            else
              PasswordInput.classList.add("is-valid")
            
            if(this.errorFromApi.PhoneNumber != null &&  this.errorFromApi.PhoneNumber != "")
              PhoneNumberInput.classList.add("is-invalid")
            else
              PhoneNumberInput.classList.add("is-valid")
            
            if(this.errorFromApi.DateOfBirth != null &&  this.errorFromApi.DateOfBirth != "")
              DateOfBirthInput.classList.add("is-invalid")
            else
              DateOfBirthInput.classList.add("is-valid")
            
            if(this.errorFromApi.ConfirmedPassword != null &&  this.errorFromApi.ConfirmedPassword != "")
              ConfirmedPasswordInput.classList.add("is-invalid")
            else
              ConfirmedPasswordInput.classList.add("is-valid")
            
        }
        else{
          console.error("Error occurred:", error);
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
</style>
