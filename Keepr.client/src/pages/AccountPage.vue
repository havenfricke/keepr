<template>
  <div class="container-fluid">
    <span class="row justify-content-center py-5 shadow bg-light">
      <img class="rounded img-fluid" :src="account.picture" alt="" />
      <div class="col-8 col-8-md col-12-sm">
        <div class="row justify-content-start text-truncate">
          <h1
            style="font-size: 26pt"
            class="col-12 p-2 text-start text-truncate"
          >
            {{ account.name }}
          </h1>
          <h4 class="col-12 text-start">Vaults</h4>
          <h4 class="col-12">Keeps</h4>
        </div>
      </div>
    </span>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {

  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account),

    }
  }
}
</script>

<style scoped>
img {
  width: 30vh;
  height: auto;
}
</style>
