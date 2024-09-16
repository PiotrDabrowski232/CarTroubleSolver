<template>
  <div class="panel">
    <RouterLink class="homeLink" to="/">
      <h3>Home</h3>
    </RouterLink>
    <nav>
      <RouterLink class="logoutLink" to="/" @click.prevent="Logout"
      data-bs-toggle="tooltip" data-bs-placement="bottom" data-bs-title="Logout!" >
        <i class="bi bi-door-open-fill"></i>
      </RouterLink>
      <RouterLink class="logoutLink" to="/" @click.prevent="Logout"
      data-bs-toggle="tooltip" data-bs-placement="bottom" data-bs-title="Account">
        <i class="bi bi-person"></i>
      </RouterLink>
    </nav>
  </div>
</template>

<script>
import { Tooltip } from 'bootstrap'; 
import AuthService from '@/Services/AuthService';

export default {
  name: 'AuthorizedNavBar',
  mounted() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    this.tooltipList = [];
    tooltipTriggerList.forEach(tooltipTriggerEl => {
      const tooltipInstance = new Tooltip(tooltipTriggerEl, {
        delay: { show: 0, hide: 0 } 
      });
      this.tooltipList.push(tooltipInstance);
      
      tooltipTriggerEl.addEventListener('mouseleave', () => {
        tooltipInstance.hide();
      });
    });
  },
  beforeUnmount() {
    if (this.tooltipList) {
      this.tooltipList.forEach(tooltip => {
        tooltip.dispose();
      });
    }
  },
  methods: {
    Logout() {
      AuthService.removeToken();
      this.$router.push("/"); 
      this.$emit('refresh'); 
    }
  }
}
</script>

<style scoped>
@import '../Unauthorized/navsStyle.css';
</style>
