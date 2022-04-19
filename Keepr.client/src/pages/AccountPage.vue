<template>
  <div class="container-fluid">
    <span class="row justify-content-center py-5 mx-3 mt-2 shadow bg-light">
      <img class="imgfix rounded img-fluid" :src="account.picture" alt="" />
      <div class="col-8 col-8-md col-12-sm">
        <div class="row justify-content-start text-truncate">
          <h1
            style="font-size: 26pt"
            class="col-12 p-2 text-start text-truncate"
          >
            {{ account.name }}
          </h1>
          <h4 class="col-12 text-start">Vaults: {{ vaultCount }}</h4>
          <h4 class="col-12">Keeps: {{ userKeepCount }}</h4>
        </div>
      </div>
    </span>
    <div class="d-flex justify-content-center">
      <div
        style="
          height: 55vh;
          width: 98vw;
          border: 1px solid #ccc;
          overflow: auto;
        "
        class="row mt-5"
      >
        <div
          v-for="v in myvaults"
          :key="v.id"
          class="col-lg-3 col-md-6 col-sm-12 hoverable justify-content-center"
        >
          <VaultCard :vault="v" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"


export default {

  name: 'Account',
  setup() {

    watchEffect(async () => {
      try {
        await keepsService.getUserKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      account: computed(() => AppState.account),
      myvaults: computed(() => AppState.myvaults),
      vaultCount: computed(() => AppState.myvaults.length),
      userKeeps: computed(() => AppState.profilekeeps),
      userKeepCount: computed(() => AppState.profilekeeps.length)
    }
  }
}
</script>

<style scoped>
.imgfix {
  width: 30vh;
  height: auto;
}
.hoverable:hover {
  transform: scale(1.001);
  filter: drop-shadow(0px 15px 10px rgba(0, 0, 0, 0.3));
  transition: 125ms ease-in-out;
  cursor: pointer;
}
.hoverable:active {
  transform: scale(0.98);
  transition: 50ms ease-in-out;
}
</style>
