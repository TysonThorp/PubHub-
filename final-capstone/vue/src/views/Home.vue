<template>
      <div>
        <div v-if="this.$store.state.user.role != null">
          <h1>Welcome, {{this.$store.state.user.username}}!</h1>
      </div>
      <div v-else>
        <h1>Home</h1>
      </div>
        
        <main-page-carousel/>
        <h2>Brewery of the Month:</h2>
        <brewery-detail :breweryId="parseInt(2)" :showFull="false" />
        <!--<v-container>
          <v-layout row wrap>
            <v-flex xs12 md6>
              <v-btn outline block class="primary">HELLO</v-btn>
            </v-flex>
            <v-flex xs4 md>
              <v-btn outline block class="primary">HI</v-btn>
            </v-flex>  
            <v-flex xs4 md>
              <v-btn outline block class="primary">HI</v-btn>
            </v-flex>
            <v-flex xs4 md>
              <v-btn outline block class="primary">HI</v-btn>
            </v-flex>
            <v-flex xs4 md>
              <v-btn outline block class="primary">HI</v-btn>
            </v-flex>
          
          <v-layout row wrap justify-center>
            <v-flex xs12>
              <v-btn outline block class="secondary">testing</v-btn>
            </v-flex>  
          </v-layout>
        
        </v-layout>
        </v-container>-->
        
      </div>
</template>

<script>


import MainPageCarousel from '../components/MainPageCarousel.vue'
// import BreweryList from '../components/BreweryList.vue'
import BreweryDetail from '../components/BreweryDetail.vue'


  export default {
    name: 'home',
    components: { 
      MainPageCarousel,
      // BreweryList,
      BreweryDetail 
    }, 
    data: () => ({ drawer: null }),
  }

 
</script>

<style>
 
</style>
