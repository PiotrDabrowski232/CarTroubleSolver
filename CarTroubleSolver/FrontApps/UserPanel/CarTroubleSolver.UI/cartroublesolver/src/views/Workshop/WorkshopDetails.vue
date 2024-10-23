<template>
    <div>
        <button type="button" @click="checkWorkshops" class="btn btn-outline-dark">Ceck Workshops</button>

        

    </div>
</template>


<script>
import router from '@/router';

export default {
  name: 'WorskhopDetails',
  data() {
    return {

    };
  },
  methods: {
    checkWorkshops(){
        router.push("/WorkshopList")
    }
  }
}
</script>