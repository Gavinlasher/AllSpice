<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-3" v-for="r in recipes" :key="r.id">
        <Recipes :recipe="r" />
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 d-flex justify-content-end p-3">
        <button
          class="btn btn-primary rounded-pill"
          data-bs-toggle="modal"
          data-bs-target="#create-recipe"
        >
          +
        </button>
      </div>
    </div>
  </div>
  <Modal id="create-recipe">
    <template #title> Create Recipe </template>
    <template #body><RecipeForm /></template>
  </Modal>
</template>

<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from '../services/RecipesService';
import { AppState } from "../AppState";
export default {
  name: 'Home'
  ,
  setup() {
    watchEffect(async () => {
      try {
        await recipesService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, "error message")
      }
    })
    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
