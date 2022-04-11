<template>
  <div class="rounded shadow mt-2 bg-light hoverable selectable">
    <div class="text-start d-flex justify-content-between">
      <h6 class="p-1">{{ recipe.category }}</h6>

      <i class="mdi mdi-heart text-dark p-1"></i>
    </div>
    <img class="img-fluid" :src="recipe.picture" />
    <div class="cw-bg">
      <h5>
        {{ recipe.title }}
      </h5>
      <h6>{{ recipe.subtitle }}</h6>
    </div>
    <div class="d-flex justify-content-end p-2">
      <button
        v-if="account.id == recipe.creatorId"
        @click="remove(recipe.id)"
        class="btn btn-danger me-3"
      >
        Delete Recipe
      </button>
      <button
        class="btn btn-primary"
        data-bs-toggle="modal"
        :data-bs-target="'#see-more' + recipe.id"
        @click="getSteps(recipe.id)"
      >
        See More
      </button>
    </div>
  </div>
  <Modal :id="'see-more' + recipe.id">
    <template #title>
      <span class="text-center"> Recipe Details</span>
    </template>
    <template #body><RecipeDetails :id="recipe.id" /></template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { stepsService } from "../services/StepsService"
export default {
  props: {
    recipe: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      async getSteps(id) {
        try {
          await stepsService.getSteps(id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async remove(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.remove(id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, "error message")
        }
      },
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.background {
  background: v-bind();
}
.cw-bg {
  background: rgba(128, 128, 128, 0.297);
  border-radius: 3px;
}
img {
  height: 40vh;
  width: 100%;
  object-fit: cover;
}
.selectable:hover {
  color: grey;
}
</style>