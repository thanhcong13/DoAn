<template>
  <div class="logo">
    <RouterLink to="/">
      <img style="width:180px; height:auto" src="../../../assets/images/logos//qcs-auto-logo.png" />
    </RouterLink>
  </div>
</template>
<script setup lang="ts">
import { RouterLink } from "vue-router";
const user = localStorage.getItem("username");
</script>
