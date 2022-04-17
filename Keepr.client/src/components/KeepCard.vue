<template>
  <img
    @click="getKeepById"
    data-bs-toggle="modal"
    data-bs-target="#myModal"
    class="img-fluid rounded shadow"
    :src="keep.img"
    alt=""
  />
  <h5 class="text-white p-0 my-0 ms-3" style="transform: translateY(-4.5vh)">
    <b>{{ keep.name }}</b>
  </h5>
</template>

<script>
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keep.id)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style>
div,
img {
  overflow-x: hidden;
}
</style>