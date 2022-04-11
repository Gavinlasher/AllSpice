import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class StepsService {
  async getSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    logger.log("steps new", res.data)
    AppState.steps = res.data
  }
  async getIngredients(id) {
    const res = await api.get("api/recipes/" + id + "/ingredients")
    logger.log(res.data)
    AppState.ingredients = res.data
  }
  async addStep(body) {
    const res = await api.post("api/steps", body)
    logger.log(res.data)
    AppState.steps = [...AppState.steps, res.data]

  }
  async addIngredients(body) {
    const res = await api.post("api/ingredients", body)
    logger.log(res.data)
    AppState.ingredients = [...AppState.ingredients, res.data]
  }
  async removeStep(id) {
    const res = await api.delete("api/steps/" + id)
    logger.log(res.data)
    AppState.steps = AppState.steps.filter(s => s.id != id)
  }
  async removeIngredient(id) {
    const res = await api.delete("api/ingredients/" + id)
    logger.log(res.data)
    AppState.ingredients = AppState.ingredients.filter(i => i.id != id)
  }
}
export const stepsService = new StepsService();