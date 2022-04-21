<template>
  <img
    @click="getKeepById"
    data-bs-toggle="modal"
    data-bs-target="#KVModal"
    class="img-fluid rounded shadow my-2"
    :src="keep.img"
    alt=""
  />
  <h5
    class="text-white p-0 my-0 ms-3 justify-content-start row"
    style="transform: translateY(-4.5vh)"
  >
    <b class="tshadow text-truncate text-start px-1 mx-1 col-8">{{
      keep.name
    }}</b>
  </h5>
</template>

<script>
import { AppState } from "../AppState"

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
          AppState.activeKeep = {}
          await keepsService.getKeepById(props.keep.id)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
div,
img {
  overflow-x: hidden;
}
.tshadow {
  text-shadow: rgb(0, 0, 0) 1px 2px 6px;
}
</style>