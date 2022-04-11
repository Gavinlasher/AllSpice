<template>
  <div class="row bg-light">
    <div class="col-md-4">
      <img
        class="img-fluid rounded shadow"
        :src="currentRecipe.picture"
        alt=""
      />
    </div>
    <div class="col-md-8">
      <div class="row">
        <div class="col-md-12">
          <h5>{{ currentRecipe.title }}</h5>
          <p>{{ currentRecipe.subtitle }}</p>
        </div>
        <div class="col-md-6 text-dark">
          <div class="bgcolor text-light rounded-top">
            <h6 class="text-center m-0">Recipe Steps</h6>
          </div>
          <div class="bggrey">
            <h6
              v-for="s in steps"
              :key="s.id"
              class="selectable"
              @click="removeStep(s.id)"
            >
              {{ s.position }} {{ s.body }}
            </h6>
          </div>
          <div>
            <form @submit.prevent="addStep(currentRecipe.id)">
              <label for="" class="form-label"></label>
              <input
                type="text"
                v-model="editable.body"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Add step..."
              />
              <button type="submit" class="btn greenbtn">+</button>
            </form>
          </div>
        </div>
        <div class="col-md-6 text-dark">
          <div class="bgcolor text-light rounded-top">
            <h6 class="text-center m-0">Ingredients</h6>
          </div>
          <div class="bggrey">
            <h6
              v-for="i in ingredients"
              :key="i.id"
              class="selectable"
              @click="removeIngredient(i.id)"
            >
              {{ i.name }} {{ i.quantity }}
            </h6>
          </div>
          <div>
            <form @submit.prevent="addIngredients(currentRecipe.id)">
              <label for="" class="form-label"></label>
              <input
                type="text"
                v-model="editable.name"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Name of Ingredient..."
              />
              <label for="" class="form-label"></label>
              <input
                type="text"
                v-model="editable.quantity"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Add Quantity..."
              />
              <button type="submit" class="btn greenbtn">+</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { stepsService } from "../services/StepsService"
export default {
  props: {
    id: {
      type: Number,
      required: true
    }
  },

  setup(props) {
    const editable = ref({
      recipeId: props.id
    })
    watchEffect(async () => {
      try {
        // await stepsService.getAllSteps(props.id)
      } catch (error) {
        logger.error(error, 'error message')
        Pop.toast(error.message)
      }
    })
    return {
      editable,
      async addStep() {
        try {
          await stepsService.addStep(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async addIngredients() {
        try {
          await stepsService.addIngredients(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async removeStep(id) {
        try {
          if (await Pop.confirm()) {

            await stepsService.removeStep(id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async removeIngredient(id) {
        try {
          if (await Pop.confirm()) {

            await stepsService.removeIngredient(id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      account: computed(() => AppState.account),
      currentRecipe: computed(() => AppState.recipes.find(r => r.id == props.id)),
      recipe: computed(() => AppState.recipes.find(r => r.id == props.Id)),
      steps: computed(() => AppState.steps),
      ingredients: computed(() => AppState.ingredients)
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 50vh;
  width: 100%;
  object-fit: cover;
}
h5 {
  color: green;
}
p {
  color: gray;
}
.bgcolor {
  background-color: rgb(91, 153, 91);
}
.bggrey {
  background-color: rgba(164, 162, 162, 0.668);
}
.greenbtn {
  background-color: green;
}
.selectable:hover {
  color: rgba(246, 55, 55, 0.667);
}
</style>