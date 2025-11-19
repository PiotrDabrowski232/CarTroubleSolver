<template>
    <div class="message-response">
      <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
      <h4>Message Details</h4>
      <div v-if="Message !== null && Message !== undefined" class="message-info">
        <p><strong>Service:</strong> {{ Message.service }}</p>
        <p><strong>Sender:</strong> {{ Message.userName }}</p>
        <p><strong>Phone:</strong> {{ Message.telephone }}</p>
        <p><strong>Car:</strong> {{ Message.car.brand }} {{ Message.car.model }}</p>
        <p><strong>Mileage:</strong> {{ Message.car.mileage }} km</p>
        <p><strong>Engine:</strong> {{ Message.car.engine }}</p>
        <p><strong>Description:</strong> {{ Message.description }}</p>
        <p><strong>Received:</strong> {{ ConvertDate(Message.sendDay) }}</p>
      </div>
  
      <div class="comment-section">
        <label for="comment">Your Comment:</label>
        <textarea id="comment" v-model="comment" rows="4" placeholder="Write your comment here..."></textarea>
  
        <label for="arrival-date">Select Arrival Date:</label>
        <input type="date" id="arrival-date" v-model="arrivalDate" min="today"/>
      </div>
  
      <div class="button-group">
        <button type="button" @click="sendResponse" class="btn btn-primary">Send Response</button>
        <button type="button" @click="cancelResponse" class="btn btn-secondary">Cancel</button>
      </div>
    </div>
  </template>
  
  <script>
  import { MessageFullInfo, setRead, sendMessage } from '@/ApiCommunication/Message';
  import Toast from '@/components/Toast.vue';

  export default {
    name: 'MessageResponse',
    components: { Toast },
    props: {
      id: String,
    },
    data() {
      return {
        Message: null,
        comment: '',
        arrivalDate: '', 
        toastMsg : '',
        showToast : false,
        toastColor : ''
      };
    },
    mounted() {
      this.getMessage();
      if(this.Message !==null && !this.Message.isRead){
        this.setAsRead();
      }
      this.arrivalDate = new Date().toISOString().split('T')[0]; 
    },
    methods: {
      showToastMessage(message, color) {
      this.toastMsg = message;
      this.showToast = true;
      this.toastColor = color;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
      async getMessage() {
        this.Message = await MessageFullInfo(this.id);
        console.log(this.Message);
      },
      async setAsRead() {
        await setRead(this.id);
      },
      async sendResponse() {
        const message = {
          description: this.comment,
          date: this.arrivalDate
        }
        var response = await sendMessage(this.id, message)
       if(response){
        this.showToastMessage("message send successfully", "success")
        this.$router.push("/ReceiveMessage")
       }
      },
      cancelResponse() {
        this.comment = '';
        this.arrivalDate = ''; 
        console.log('Response cancelled');
      },
      ConvertDate(dateTime) {
        const date = new Date(dateTime);
        return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear()} ${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}`;
      },
    },
  };
  </script>
  
  <style scoped>
  .message-response {
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    margin: 20px;
  }
  
  .message-info {
    margin-bottom: 20px;
  }
  
  .comment-section {
    margin-bottom: 20px;
  }
  
  textarea {
    width: 100%;
    margin-top: 8px;
  }
  
  input[type="date"] {
    width: 100%;
    margin-top: 8px;
  }
  
  .button-group {
    display: flex;
    justify-content: space-between;
  }
  
  .btn {
    padding: 10px 15px;
  }
  </style>
  