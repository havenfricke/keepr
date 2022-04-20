<template>
  <div class="container-fluid">
    <div class="masonry-columns mt-0 p-2">
      <div
        v-for="k in keeps"
        :key="k.id"
        class="col-12 rounded hoverable container"
      >
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute();
    if (route.params.id) {
      watchEffect(async () => {
        try {
          await keepsService.getVaultKeeps(route.params.id)
        } catch (error) {
          logger.log(error)
        }
      });
    }
    return {}
  }
}
</script>
