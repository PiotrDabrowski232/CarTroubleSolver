<template>
<form class="needs-validation" novalidate>
  <div class="container">
   
      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Surname" type="text" class="form-control" id="floatingSurname" placeholder="Surname">
          <label for="floatingSurname">Surname</label>
          <div class="invalid-feedback">
            {{errorFromApi.Surname}}
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.Name" type="text" class="form-control" id="floatingName" placeholder="Name">
          <label for="floatingName">Name</label>
          <div class="invalid-feedback">
            {{errorFromApi.Name}}
          </div>
        </div>
      </div>

      <div class="form-floating mb-3">
        <input v-model="User.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com">
        <label for="floatingEmail">Email address</label>
        <div class="invalid-feedback">
          {{errorFromApi.Email}}
          </div>
      </div>

      <div class="form-floating mb-3">
        <input v-model="User.UserName" type="text" class="form-control" id="floatingUserName" placeholder="">
        <label for="floatingUserName">User Name</label>
          <div class="invalid-feedback">
            {{errorFromApi.UserName}}
          </div>
      </div>

      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Password</label>
          <div class="invalid-feedback">
            {{errorFromApi.Password}}
          </div>
        </div>

        <div class="form-floating">
          <input v-model="User.ConfirmedPassword" type="password" class="form-control" id="floatingConfirmedPassword" placeholder="Confirm password">
          <label for="floatingConfirmedPassword">Confirm Password</label>
          <div class="invalid-feedback">
            {{errorFromApi.ConfirmedPassword}}
          </div>
        </div>
      </div>

      <div class="form-floating">
          <input v-model="User.DateOfBirth" type="date" class="form-control" id="Date Of Birth" placeholder="Date Of Birth">
          <label for="Date Of Birth">Date Of Birth</label>
          <div class="invalid-feedback">
            {{errorFromApi.DateOfBirth}}
          </div>
        </div>

      <div class="form-floating">
        <input v-model="User.PhoneNumber"  type="number" :minlength="9" :maxlength="9" class="form-control" id="PhoneNumber" placeholder="PhoneNumber">
        <label for="PhoneNumber">Phone Number</label>
          <div class="invalid-feedback">
            {{errorFromApi.PhoneNumber}}
          </div>
      </div>

        <button @click="postPost" type="button" class="btn btn-primary mt-5">Create Account</button>
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
          console.log("Validation errors:");
            console.log(error.response.data.errors.Email);
            this.errorFromApi.UserName = error.response.data.errors.UserName,
            this.errorFromApi.Name = error.response.data.errors.Name,
            this.errorFromApi.Email = error.response.data.errors.Email,
            this.errorFromApi.Password = error.response.data.errors.Password,
            this.errorFromApi.PhoneNumber = error.response.data.errors.PhoneNumber,
            this.errorFromApi.DateOfBirth = error.response.data.errors.DateOfBirth,
            this.errorFromApi.ConfirmedPassword = error.response.data.errors.ConfirmedPassword
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
