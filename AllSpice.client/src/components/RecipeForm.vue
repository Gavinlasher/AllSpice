<template>
  <form class="" @submit.prevent="createRecipe">
    <div class="col-md-12">
      <label for="" class="form-label"> Title </label>
      <input
        type="text"
        v-model="editable.title"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Recipes Title here..."
      />
    </div>
    <div class="col-md-12">
      <label for="" class="form-label"> Category </label>
      <input
        type="text"
        v-model="editable.category"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Recipes Category here..."
      />
    </div>
    <div class="col-md-12">
      <label for="" class="form-label"> Subtitle </label>
      <input
        type="text"
        v-model="editable.subtitle"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Recipes Title here..."
      />
    </div>
    <div class="col-md-12">
      <label for="" class="form-label"> Add Picture </label>
      <input
        type="text"
        v-model="editable.picture"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Put link here..."
      />
    </div>
    <button class="btn greenbtn rounded text-light mt-3" type="submit">
      Create Recipe
    </button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createRecipe() {
        try {
          await recipesService.createRecipe(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, "error message")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.greenbtn {
  background-color: green;
}
</style>