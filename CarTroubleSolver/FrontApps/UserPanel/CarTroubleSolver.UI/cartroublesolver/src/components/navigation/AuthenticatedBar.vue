<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <router-link class="navbar-brand router-link" to="/" exact-path>Home</router-link>
    <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
      <div class="dropdown">
        <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <i class="pi pi-user" style="font-size: 1.5rem; color:darkgreen;"></i>
        </button>
        <ul class="dropdown-menu">
          <li class="dropdown-item">
            <router-link v-on:click="Logout()" class="router-link" to="/" exact-path>Logout</router-link>
          </li>
          <li class="dropdown-item">
            <router-link class="router-link" to="/UserInfo" exact-path>User Info</router-link>
          </li>
        </ul>
      </div>

      <button type="button" class="btn position-relative" data-bs-toggle="offcanvas" data-bs-target="#staticBackdrop" aria-controls="staticBackdrop">
        <i class="pi pi-envelope" style="font-size: 1.5rem; color:darkgreen;"></i>
        <span v-if="hasUnreadMessages()" class="position-absolute top-0 start-100 translate-middle p-2 bg-danger border border-light rounded-circle">
          <span class="visually-hidden">New alerts</span>
        </span>
      </button>
    </div>
  </nav>

  <div class="offcanvas offcanvas-start" data-bs-backdrop="static" tabindex="-1" id="staticBackdrop" aria-labelledby="staticBackdropLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="staticBackdropLabel">Message Responses</h5>
      <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body">
      <div v-if="messages.length === 0" class="text-center">
        <p>Your inbox is empty</p>
      </div>
      <div v-for="message in messages" :key="message.id" class="mb-3 w-100">
        <div :class="['card w-100', { 'bg-light': message.isRead, 'bg-warning': !message.isRead }]">
          <div class="card-body">
            <h5 class="card-title"><strong>Sender: {{ message.workshopName }}</strong></h5>
            <p class="card-text"><strong>Sent At:</strong> {{ formatDate(message.sentAt) }}</p>
            <p class="card-text"><strong>Service:</strong> {{ message.service}}</p>
            <p class="card-text"><strong>Vehicle:</strong> {{ message.brand }} {{ message.model }}</p>
          </div>
          <button type="button" class="btn btn-secondary" v-on:click="GoToMessage(message.id)">Read More</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AuthService from '../../services/AuthService';
import { receiveMessage } from '@/services/UserApiCommunication';

export default {
  name: 'AuthenticatedBar',
  data() {
    return {
      messages: []
    };
  },
  mounted() {
    this.interval = setInterval(this.getMessages, 5000);
  },
  beforeUnmount() {
    clearInterval(this.interval);
  },
  methods: {
    hasUnreadMessages() {
      return this.messages.some(message => !message.isRead);
    },
    Logout() {
      AuthService.removeToken();
      localStorage.clear();
      this.$router.push("/");
      this.$emit('refresh');
    },
    async getMessages() {
      const receivedMessages = await receiveMessage();
      this.messages = receivedMessages; 
    },
    formatDate(dateString) {
      const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
      return new Date(dateString).toLocaleDateString(undefined, options);
    },
    GoToMessage(id){
      this.$router.push({ name: 'MessageDetails', params: { id: id } });
    }
  }
}
</script>

<style>
body {
  margin: auto;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  width: 100%;
  height: 8vh;
  border-bottom: 1px solid black;
}

#navbarNav {
  margin-right: 4vw;
}

.dropdown {
  margin-right: vw;
}

.navbar-nav {
  margin-top: 1.2vh;
  padding-right: 2vw;
}

.dropdown-item .router-link {
  color: black;
  text-decoration: none;
}

.main {
  width: 100%;
  min-height: 87vh;
}

.router-view {
  height: 100%;
  width: 100%;
}

.footer {
  background-color: rgb(0, 0, 0);
  width: 100%;
  height: 5vh;
}

button {
  background-color: white;
  border: 0px;
}

.card {
  border-radius: 10px;
}

.card.bg-warning {
  border: 1px solid #ffc107; 
}

.card.bg-light {
  border: 1px solid #d3d3d3; 
}

.offcanvas-body {
  margin: 0;
  padding: 0px;
}

.w-100 {
  width: 100% !important; 
}
</style>
