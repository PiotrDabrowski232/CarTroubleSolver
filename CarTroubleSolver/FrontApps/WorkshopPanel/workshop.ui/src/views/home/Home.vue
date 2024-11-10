<template>
  <div class="container">
    <div v-if="isAuthenticated" class="tile">
      <RouterLink to="/ReceiveMessage" class="Homelink">
        <i class="bi bi-envelope-fill icon"></i>
        <h2>Messages</h2>
        <p>User inquiries about performing a service in your workshop</p>
      </RouterLink>
    </div>

    <div v-if="isAuthenticated" class="tile">
      <RouterLink to="/Accidents" class="Homelink">
        <i class="bi bi-tools icon"></i>
        <h2>Accidents</h2>
        <p>Report and view accident history</p>
      </RouterLink>
    </div>

    <div v-if="isAuthenticated" class="tile">
      <RouterLink to="/Ratings" class="Homelink">
        <i class="bi bi-star-fill icon"></i>
        <h2>My opinions</h2>
        <p>User reviews of your workshop</p>
      </RouterLink>
    </div>

    <p v-else class="login-message">Please log in to access these features.</p>
  </div>
</template>

<script>
  import AuthService from '@/Services/AuthService';

export default {
  name: 'Home',
  data() {
    return {
      isAuthenticated: false
    }
  },
  created() {
    this.isAuthenticated = AuthService.getToken();
    console.log(this.isAuthenticated )

  }
}
</script>

<style scoped>
.container {
  display: flex;
  justify-content: space-around;
  align-items: center;
  flex-wrap: wrap;
  gap: 30px;
  padding: 40px;
}

.tile {
  text-align: center;
  width: 250px;
  height: 250px;
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color: #e0e0e0;
  border-radius: 12px;
  box-shadow: 0px 6px 12px rgba(0, 0, 0, 0.15);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.tile:hover {
  transform: scale(1.1);
  box-shadow: 0px 8px 16px rgba(0, 0, 0, 0.2);
}

.icon {
  font-size: 50px;
  color: #333;
  margin-bottom: 15px;
}

h2 {
  margin: 10px 0 5px;
  font-size: 20px;
  color: #333;
}

p {
  margin: 0;
  font-size: 14px;
  color: #666;
}

.Homelink {
  text-decoration: none;
  color: inherit;
}

.login-message {
  color: #999;
  font-size: 16px;
  text-align: center;
  width: 100%;
}
</style>
