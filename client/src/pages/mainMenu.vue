<template>

  <div class="main-main">

    <h1 class="main-title">T-rex Multiplayer</h1>

    <div class="main-buttons">
      <button @click="this.$router.push('/join')" class="btn primary-button">Join</button>
      <button @click="this.$router.push('/host')" class="btn primary-button">Host</button>
    </div>

    <hallOfFame />

  </div>

</template>

<script>
import hallOfFame from '@/components/hallOfFame.vue'

export default {
  name: 'mainMenu',
  components: {
    hallOfFame
  }
}

</script>


<style>
.main-main {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  height: 90%;
}

.main-title {
  font-size: 80px;
  margin: 50px;
}

.main-buttons {
  display: flex; flex-direction: row;
  gap: 100px;
}

/* Mobile/Tablet support */

@media screen and (max-width: 900px) {
  .main-title {
    font-size: 8vw;
  }
}

@media screen and (max-width: 600px) {
  .main-buttons {
    flex-direction: column;
    gap: 20px;
  }
}
</style>