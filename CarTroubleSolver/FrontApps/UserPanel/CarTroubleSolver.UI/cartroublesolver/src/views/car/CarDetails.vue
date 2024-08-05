<template>
  <div>

    <div class="CarDetais">
      <p><strong :style="[getCarColor(this.car.color)]">VIN: </strong> {{ this.car.vin}}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Brand: </strong> {{ this.car.brand }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Model: </strong> {{ this.car.model }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Type: </strong> {{ this.car.carType }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Production Year: </strong> {{ this.convertDate(this.car.dateOfProduction) }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Door Count: </strong> {{ this.car.doorCount }}</p>
    </div>

    <div class="details-functional-buttons">
        <button type="button" class="btn btn-outline-danger">Delete Car</button>
        <button type="button" class="btn btn-outline-warning">Update Car Details</button>
    </div>

    <div class="visit-section">

      <h4><strong>Mechanical Visits...</strong></h4>
        <div class="visit-cardSection">
          <div class="card">
            <div class="card-body" :style="visitStatus(visitationType)">
              <h5 class="card-title"><strong>Type of Visit: Zmiana Oleju</strong></h5>
              <p class="card-text">Date: </p>
              <p class="card-text">Time: </p>
              <p class="card-text">Place: </p>
            </div>
          </div>
        </div>

      <Paginator class="paginator" v-model:first="first" :rows="1" :totalRecords="12"></Paginator>

    </div>


  </div>
</template>

<script>
export default {
  name: 'CarDetails',
  data() {
    return {
      car: null,
    };
  },
  created() {
    this.fetchCarByVIN();
  },
  methods: {
    fetchCarByVIN() {
      const userCars = JSON.parse(localStorage.getItem("userCars")) || [];
      const car = userCars.find(car => car.vin == this.$route.params.vin);
      console.log(car)
      if (car) {
        this.car = car;
      } else {
        console.warn("No car found with the specified VIN");
      }
    },
    getCarColor(color) {
      return `color: rgb(${color.red}, ${color.green}, ${color.blue})`;
    },
    convertDate(date){
      return new Date(date).getFullYear().toString();
    },
    visitStatus(visit){
      if(visit==="finished"){
        return `background-color:  rgb(15, 156, 15);`
      }
      else if(visit==="inProgres"){
        return `background-color:  rgb(210, 201, 25);`
      }
      else{
        return `background-color:  rgb(86, 168, 250);`
      }
    }
  }
}
</script>

<style>
.main{
  margin: 0px;
}
.CarDetais{
  text-align: left;
  width: fit-content;
  height: fit-content;
  padding-top: 4vh;
}

.CarDetais p {
  font-size: 150%;
  margin-top: 1.2rem;
  margin-left: 4vw;
}

.visit-cardSection{
  width: 90vw;
  margin:auto;
  border-radius: 15%;
}

.visit-cardSection .card{
  border: 2px solid black;
}

.paginator{
  border: none;
  margin-top: 6.5vh;
}
.visit-section{
  padding-top: 7vh;
}

.visit-section h4{
  text-align: left;
  padding-left: 4vw;
}

.visit-section .visit-cardSection{
  padding-left: 6vw;
  padding-right: 6vw;
}

.details-functional-buttons{
  top: 12vh;
  right: 10vw;
  position: absolute;
}

.details-functional-buttons .btn{
  width: 20vw;
  font-size: 1.3vw;
  letter-spacing: 0.125vw;
  display: block;
  margin-top: 2vh;
}

</style>
