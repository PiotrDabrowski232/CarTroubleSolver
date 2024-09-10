<template>
  <div class="form-floating mb-3">
    <select class="form-select" 
      :style="[`width: ${width}vw;`]"
      v-bind="$attrs"
      :value="modelValue"
      @change="$emit('update:modelValue', $event.target.value)"
    >
      <option value="" disabled selected>Select {{label}}</option>
      <option v-for="(data, index) in dataArray" :key="index" :value="data">
        {{ data }}
      </option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'SelectInput',
  props: {
    label: {
      type: String,
      required: true,
      default: '',
    },
    modelValue: {
      type: String,
      required: true,
      default: '', 
    },
    width: {
      type: Number,
      default: 1,
    },
    dataArray: {
      type: Array,
      default: () => [],
    },
  },
};
</script>

<style scoped>
@import './style.css';
</style>
