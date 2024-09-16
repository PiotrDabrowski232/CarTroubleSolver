<template>
    <AuthorizedNavBar @refresh="refreshParent" v-if="this.isAuthenticated"/>
    <UnauthorizedNavBar v-if="!this.isAuthenticated"/>
</template>
  
  <script>
  import UnauthorizedNavBar from './Unauthorized/UnauthorizedNavBar.vue'
  import AuthService from '@/Services/AuthService';
  import AuthorizedNavBar from './Authorized/AuthorizedNavBar.vue'

  export default {
    name: 'BaseNav',
    components:{AuthorizedNavBar, UnauthorizedNavBar},
  data() {
    return {
      isAuthenticated: AuthService.getToken() 
    };
  },
  computed: {
    userComponent(){
      console.log(this.isAuthenticated)
      return this.isAuthenticated ;
    }
  },
  watch: {
    '$route'() {
      this.isAuthenticated = AuthService.getToken();
    }
  },
  methods:{
    refreshParent() {
      this.isAuthenticated = AuthService.getToken();
    }
  }
  }
  </script>
  