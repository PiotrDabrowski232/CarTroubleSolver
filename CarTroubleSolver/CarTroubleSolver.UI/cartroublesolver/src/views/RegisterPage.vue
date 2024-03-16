<template>
  <div class="container">

    <form>

      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Surname" type="text" class="form-control" id="floatingSurname" placeholder="Surname">
          <label for="floatingSurname">Surname</label>
        </div>

        <div class="form-floating">
          <input v-model="User.Name" type="text" class="form-control" id="floatingName" placeholder="Name">
          <label for="floatingName">Name</label>
        </div>
      </div>

      <div class="form-floating mb-3">
        <input v-model="User.Email" type="email" class="form-control" id="floatingEmail" placeholder="name@example.com">
        <label for="floatingEmail">Email address</label>
      </div>

      <div class="form-floating mb-3">
        <input v-model="User.UserName" type="text" class="form-control" id="floatingUserName" placeholder="">
        <label for="floatingUserName">User Name</label>
      </div>

      <div class="d-flex">
        <div class="form-floating">
          <input v-model="User.Password" type="password" class="form-control" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Password</label>
        </div>

        <div class="form-floating">
          <input v-model="User.ConfirmedPassword" type="password" class="form-control" id="floatingConfirmedPassword" placeholder="Confirm password">
          <label for="floatingConfirmedPassword">Confirm Password</label>
        </div>
      </div>

      <VueDatePicker v-model="date" teleport-center  :format="format" />

      <div class="form-floating">
        <input v-model="User.PhoneNumber"  type="number" :minlength="9" :maxlength="9" class="form-control" id="PhoneNumber" placeholder="PhoneNumber">
        <label for="PhoneNumber">Phone Number</label>
      </div>

      <button type="submit">Add item</button>

    </form>

  </div>
</template>

<script setup>
import { ref } from 'vue';

const date = ref(new Date());
const format = (date) => {
  const day = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();
  return `Selected date is ${day}/${month}/${year}`;
}

</script>

<script>
import VueDatePicker from '@vuepic/vue-datepicker';
import axios from 'axios';

export default {
  name: "RegisterPage",
  data() {
    return {
      User:{
        UserName:"",
        Name: "",
        Surname: "",
        Email:"",
        Password: "",
        PhoneNumber:0,
        DateOfBirth: this.date,
        ConfirmedPassword:"",
      },
      errors: []
    }
  },

  methods: {
    // Pushes posts to the server when called.
    async postPost() {
      console.log(this.User)
      await axios.post(`http://localhost:5113/Register`, {
        body: this.User
      })
      .then(response => {console.log(response)})
      .catch(e => {
        this.errors.push(e)
      })
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
  margin-right: 10px;
}

.d-flex {
  display: flex;
}
</style>
