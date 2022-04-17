<template>
  <img class="img-fluid rounded shadow" :src="keep.img" alt="" />
  <h5 class="text-white p-0 my-0 ms-3" style="transform: translateY(-4.5vh)">
    <b>{{ keep.name }}</b>
  </h5>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style>
</style>