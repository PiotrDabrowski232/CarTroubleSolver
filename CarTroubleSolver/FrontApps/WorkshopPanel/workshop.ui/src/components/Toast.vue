<template>
  <div v-show="visible" :class="[`toast align-items-center text-bg-${color} border-0 show`]">
    <div class="d-flex">
      <div class="toast-body">
        {{ msg }}
      </div>
      <button type="button" class="btn-close btn-close-white me-2 m-auto" @click="hideToast" aria-label="Close"></button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Toast',
  props: {
    msg: {
      type: String,
      required: true,
    },
    color: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      visible: true,
    };
  },
  methods: {
    hideToast() {
      this.visible = false;
    },
  },
};
</script>

<style scoped>
.toast {
  position: fixed; 
  top: 20px; 
  right: 20px;
  z-index: 1050; 
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1); 
  transition: opacity 0.3s ease; 
}
</style>
