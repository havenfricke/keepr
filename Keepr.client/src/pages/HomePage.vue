<template>
  <div class="container-fluid">
    <div class="masonry-columns p-2">
      <div
        v-for="k in keeps"
        :key="k.id"
        class="col-12 rounded hoverable container-fluid"
      >
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity"
import { watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  name: 'Home',
  setup() {
    watchEffect(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }

  }
}
</script>

<style scoped lang="scss">
.hoverable:hover {
  transform: scale(1.03);
  filter: drop-shadow(0px 15px 10px rgba(0, 0, 0, 0.3));
  transition: 125ms ease-in-out;
  cursor: pointer;
}
.hoverable:active {
  transform: scale(0.98);
  transition: 50ms ease-in-out;
}
.masonry-columns {
  columns: 5 200px;
  column-gap: 1rem;
  img {
    display: inline-block;
    width: 100%;
  }
}
</style>
