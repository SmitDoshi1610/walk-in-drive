export interface personalInformation {
    firstName: string
    lastName: string
    email: string
    password: string
    phone: string
    resume: string
    portflioUrl: string
    preferredJobRoles: string[]
    referredBy: string
    emailNotification: boolean,
    profilePicture: string
}

export interface educationQualification {
    percentage: number
    passingYear: number
    qualification: string
    stream: string
    college: string
    otherCollege: string
    collegeLocation: string
}

export interface professionalQualification {
    applicationType: string
    fresherTechnologiesFamiliar: string[]
    fresherOtherFamiliar: string
    fresherRoleAppliedFor: string
    yearsOfExperience: number
    currentCTC: string
    expectedCTC: string
    technologiesExpert: string[]
    othersExpertise: string
    experienceTechnologiesFamiliar: string[]
    experienceOtherFamiliar: string
    isInNoticePeriod: string
    noticePeriodEnd: Date
    noticePeriodDuration: number
    haveAppliedTestBefore: string
    experienceRoleAppliedFor: string
}
// export interface fresher_professional_qualification {
//     technologiesFamiliar: number[]
//     others: string
//     appliedForTest: boolean
//     roleAppliedFor: string
// }

// export interface experienced_professional_qualification {
//     yearsOfExperience: string
//     currentCTC: string
//     expectedCTC: string
//     technologiesExpert: number[]
//     othersExpertise: string
//     technologiesFamiliar: number[]
//     othersFamiliar: string
//     isInNoticePeriod: boolean
//     noticePeriodEnd: Date
//     noticePeriodDuration: number
//     haveAppliedTestBefore: boolean
//     roleAppliedFor: string
// }

export interface registrationData {

    preferedJobRoles: string[]
    yearOfPassing: number[]
    qualification: string[]
    stream: string[]
    college: string[]
    technologiesExpert: any[]
    technologiesFamiliar: any[]
    noticePeriod: number[]

}

export interface ResponseMessage{

    message: string
    statusCode: number
    moveNext: boolean
}

export interface Login{
    email: string
    password: string
}

export interface User{
    id?: string
    firstname?: string
    lastname?: string
    phone?: string
}

export interface WalkInDrive{
    id: number
    name: string
    startDate: Date
    endDate: Date
    location: string
    timePreference: string
    jobPreference: string[]
    user_Id: number
    walkInJob: jobRole[]
}

export interface jobRole{
    id: number
    roleName: string
    roleDescription: string
    roleRequirement: string
    roleCompensation: number
    job_Id: number
}

export interface appliedJobRole{

    userId: number
    driveId: number
    timeSlot: string
    resume: string
    jobPreference: string[]
}