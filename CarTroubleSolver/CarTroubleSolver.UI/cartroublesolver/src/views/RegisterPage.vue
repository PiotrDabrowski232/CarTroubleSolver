<template>
    
    <div>
      <FloatLabel>
        <InputText id="username" v-model="value" />
        <label for="username">Username</label>
      </FloatLabel>
    </div>

</template>
    
<script>

    export default {
  name: 'RegisterPage',
  data(){
    return {
      component:''
    }
  }
}
</script>
    
<style>
    .p-float-label{
      width: fit-content;
    }
</style>
    