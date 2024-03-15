<template>
  <div class="container">
    <div class="d-flex">
      <div class="form-floating">
        <input type="text" class="form-control" id="floatingSurname" placeholder="Surname">
        <label for="floatingSurname">Surname</label>
      </div>

      <div class="form-floating">
        <input type="text" class="form-control" id="floatingName" placeholder="Name">
        <label for="floatingName">Name</label>
      </div>
    </div>

    <div class="form-floating mb-3">
      <input type="email" class="form-control" id="floatingEmail" placeholder="name@example.com">
      <label for="floatingEmail">Email address</label>
    </div>

    <div class="form-floating mb-3">
      <input type="text" class="form-control" id="floatingUserName" placeholder="">
      <label for="floatingUserName">User Name</label>
    </div>

    <div class="d-flex">
      <div class="form-floating">
        <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
        <label for="floatingPassword">Password</label>
      </div>

      <div class="form-floating">
        <input type="password" class="form-control" id="floatingConfirmedPassword" placeholder="Confirm password">
        <label for="floatingConfirmedPassword">Confirm Password</label>
      </div>
    </div>
    <VueDatePicker v-model="date" teleport-center  :format="format" />
  </div>
</template>

<script>
export default {
  name: 'RegisterPage',
};
</script>

<script setup>
// Example using a custom format function
import { ref } from 'vue';

const date = ref(new Date());
// In case of a range picker, you'll receive [Date, Date]
const format = (date) => {
  const day = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();

  return `Selected date is ${day}/${month}/${year}`;
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
