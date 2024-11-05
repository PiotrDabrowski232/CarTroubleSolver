<template>
    <div>
      <div class="message-grid">
        <div v-for="(message, index) in Messages" :key="index" class="card">
          <div class="card-body">
            <h5 class="card-title">{{ message.service }}</h5>
            <p v-if="message.isRead" class="card-text">
              Sender: {{ message.userName }}<br>
              Phone: {{ message.telephone }}<br>
              Car: {{ message.car.brand }} {{ message.car.model }}<br>
              {{ ConvertDate(message.sendDay) }}
            </p>
            <p v-if="!message.isRead" class="card-text">
              <strong>Sender:</strong> {{ message.userName }}<br>
              <strong>Phone:</strong> {{ message.telephone }}<br>
              <strong>Car:</strong> {{ message.car.brand }} {{ message.car.model }}<br>
              <strong> {{ ConvertDate(message.sendDay) }}</strong>
            </p>
          </div>
          <button type="button" class="btn btn-primary" v-on:click="ResponseMessage(message.id)">Response</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { getMessages } from '@/ApiCommunication/Message';

  export default {
    name: 'ReceiveMessage',
    data() {
      return {
        Messages: []
      };
    },
    mounted() {
      this.getMessages();
    },
    methods: {
      async getMessages() {
        this.Messages = await getMessages();
      },
      ResponseMessage(id){
        this.$router.push({ name: 'MessageResponse', params: { id: id } });
      },
      ConvertDate(dateTime){
        const date = new Date(dateTime);
        return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear()} ${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}`;
      }
    }
  };
  </script>
  
  <style scoped>
  .message-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 16px;
    margin-top: 20px;
    margin: 20px 2vw;
  }
  
  .card {
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }
  
  .card-body {
    padding: 16px;
  }
  
  .card-title {
    font-size: 1.2em;
    margin-bottom: 8px;
    font-weight: bold;
  }
  
  .card-text {
    font-size: 0.95em;
    color: #555;
  }
  </style>
  