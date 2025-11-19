<template>
  <div class="form-floating mb-3">
    <input
      :class="['form-control', { 'is-invalid': error && error.length > 0 }]"
      :style="[`width: ${width}vw;`]"
      class="floatingInput"
      v-bind="$attrs"
      :placeholder="label"
      :type="type"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
    />
    <label for="floatingInput">{{ label }}</label>

    <div v-if="error && error.length > 0" class="invalid-feedback">
      <ul>
        <li v-for="(err, index) in error" :key="index">{{ err }}</li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CustomInput',
  props: {
    label: {
      type: String,
      required: true,
      default: '',
    },
    modelValue: {
      type: [String, Number],
      required: true,
      default: '',
    },
    type: {
      type: String,
      required: true,
      default: 'text',
    },
    error: {
      type: Array || null,
      default: () => [],
    },
    width:{
      type: Number,
      default: 1,
    },
  },
};
</script>
<style scoped>
@import './style.css';
</style>

