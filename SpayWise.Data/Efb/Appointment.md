Appointment : BaseTable
#LocationId
#TypeId
#PatientId
#Date DateOnly
DropOff TimeOnly
Pickup DateTime
StatusId AppointmentStatus
VolumeClientId Client? // shelter/rescue/group
CopayClientId Client? // additional payer (grant)
TransportClientId Client? 
HasRabiesVaccineProof bool
KennelSizeId?
Points int
PointsClientId Client? // if null points go to clinic


