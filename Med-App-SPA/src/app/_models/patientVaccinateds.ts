export interface PatientVaccinateds {
  id: number
  patientFKId: number
  vacinesFKId: number
  timeOfVaccination: Date
  dosageMl: number
  description: string
}
