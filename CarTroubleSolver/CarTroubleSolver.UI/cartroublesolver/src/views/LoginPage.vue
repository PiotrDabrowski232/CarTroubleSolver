<template>
    
  <div class="MainContent">
    
      <FloatLabel>
          <InputText id="Name" v-model="Name" />
          <label for="Name">Name</label>
        </FloatLabel>
      <FloatLabel>
          <InputText id="Surname" v-model="Surname" />
          <label for="Surname">Surname</label>
        </FloatLabel>
      <FloatLabel>
          <InputText id="Email" v-model="Email" />
          <label for="Email">Email</label>
        </FloatLabel>
        <FloatLabel>
        <InputText id="Password" v-model="Password" />
          <label for="Password">Password</label>
        </FloatLabel>
        <FloatLabel>
          <Calendar v-model="date" style="width: fit-content;" inputId="birth_date" />
          <label for="birth_date">Birth Date</label>
        </FloatLabel>
  </div>

</template>
  
<script>
  export default {
name: 'LoginPage',
data(){
  return {
    component:''
  }
}
}
</script>

<script setup>
import { ref } from "vue";

const Name = ref(null);
const Surname = ref(null);
const Email = ref(null);
const Password = ref(null);
const date = ref();
</script>
  
<style>
.MainContent{
  padding-top: 5vh;
}
span{
  margin-top: 4vh;
}

#pv_id_3_panel{
  height: 10vh;
  width: 20vw;
}
</style>