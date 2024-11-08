<template>
  <div class="workshop-details" v-if="workshop">
    <section class="hero-section">
      <div class="hero-content">
        <h1>{{ workshop.name }}</h1>
        <div class="location" v-if="workshop.location">
          <span>{{ workshop.location.city }}, {{ workshop.location.province }}</span> 
          <span>{{ workshop.location.country }}</span>
        </div>
      </div>
    </section>

    <section class="info-section">
      <div class="info-card">
        <h2>Contact</h2>
        <p><strong>Phone number:</strong> {{ workshop.phoneNumber }}</p>
        <p><strong>NIP:</strong> {{ workshop.nip }}</p>
      </div>
      <div class="info-card">
        <h2>services</h2>
        <ul>
          <li v-for="(service, index) in workshop.services" :key="index">
            <span>{{ service.service }}</span> - <span>{{ service.price }} PLN</span>
          </li>
        </ul>
      </div>
    </section>

    <section class="reviews-section">
      <h2>Opinions</h2>
      <div v-if="workshop.rateDetails && workshop.rateDetails.length > 0">
        <div v-for="(rate, index) in workshop.rateDetails" :key="index" class="review-card">
          <p><strong>{{ rate.user }}:</strong> {{ rate.comment }}</p>
          <p><strong>Rating:</strong> {{ rate.rate }} / 5</p>
          <p><strong>Date:</strong> {{ new Date(rate.date).toLocaleDateString() }}</p>
        </div>
      </div>
      <div v-else>
        <p>Brak opinii.</p>
      </div>
    </section>
  </div>
</template>

<script>
import { WorkshopDetails } from '@/services/WorkshopCommunication';

export default {
  name: 'WorkshopDetails',
  props: {
    id: String
  },
  data() {
    return {
      workshop: {}
    };
  },
  methods: {
    async getWorkshop() {
      this.workshop = await WorkshopDetails(this.id);
    }
  },
  mounted() {
    this.getWorkshop();
  },
}
</script>

<style scoped>
.hero-section {
  background-color: #009688;
  color: white;
  padding: 60px 20px;
  text-align: center;
  margin-bottom: 30px;
}

.hero-content h1 {
  font-size: 3rem;
  font-weight: bold;
  margin-bottom: 10px;
}

.hero-content .location {
  font-size: 1.2rem;
  font-weight: 300;
}

.hero-content span {
  display: block;
  margin-top: 5px;
}

.info-section {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  padding: 20px;
  margin-bottom: 30px;
}

.info-card {
  background-color: white;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  width: 48%;
}

h2 {
  font-size: 1.6rem;
  color: #333;
  margin-bottom: 15px;
}

ul {
  list-style-type: none;
  padding: 0;
}

ul li {
  font-size: 1.1rem;
  margin-bottom: 10px;
  padding-bottom: 10px;
  border-bottom: 1px solid #eee;
}

.info-card p {
  font-size: 1.1rem;
  margin-bottom: 10px;
}

strong {
  font-weight: 600;
  color: #009688;
}

.reviews-section {
  padding: 20px;
  background-color: #f9f9f9;
}

.reviews-section h2 {
  font-size: 2rem;
  margin-bottom: 20px;
  color: #333;
}

.review-card {
  background-color: white;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  margin-bottom: 15px;
}

.review-card p {
  font-size: 1rem;
  margin-bottom: 10px;
}

.review-card strong {
  font-weight: 600;
  color: #333;
}

@media (max-width: 768px) {
  .info-section {
    flex-direction: column;
  }

  .info-card {
    width: 100%;
    margin-bottom: 20px;
  }

  .hero-content h1 {
    font-size: 2rem;
  }
}
</style>
