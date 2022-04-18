<template>
  <img
    @click="getKeepById"
    data-bs-toggle="modal"
    data-bs-target="#myModal"
    class="img-fluid rounded shadow"
    :src="keep.img"
    alt=""
  />
  <h5
    class="text-white p-0 my-0 ms-3 justify-content-around row"
    style="transform: translateY(-4.5vh)"
  >
    <b class="tshadow text-truncate px-1 mx-1 col-8">{{ keep.name }}</b>
    <img
      style="width: 50px"
      class="img-fluid object-fit me-2 col-2 rounded-circle"
      :src="keep.creator.picture"
      alt=""
    />
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
.tshadow {
  text-shadow: rgb(0, 0, 0) 1px 2px 6px;
}
</style>