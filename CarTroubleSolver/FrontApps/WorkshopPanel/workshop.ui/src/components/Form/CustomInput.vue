<template>
    <div class="form-floating mb-3">
      <input
        class="form-control"
        id="floatingInput"
        v-bind="$attrs"
        :placeholder="label"
        :type="type"
        :value="modelValue"
        @input="$emit('update:modelValue', $event.target.value)"
      />
      <label for="floatingInput">{{ label }}</label>
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
        type: [String, Number],
        required: true,
        default: 'text',
      },
    },
  };
  </script>
  